using IoCPlus;
using UnityEngine;
using System;
using System.Collections;

public class GrapplingStateView : View, IGrapplingState, ITriggerer {

    [Inject] private Ref<IGrapplingState> grappleViewRef;

    [Inject] private ActivateFloatingStateEvent activateFloatingStateEvent;
    [Inject] private ActivateSlidingStateEvent activateSlidingStateEvent;

    [SerializeField]
    private GameObject gun;

    private GrapplingHook _grapplingHook;

    private CharScriptAccess _charAccess;

    private LookAt _gunLookAt;

    private Coroutine _slingMovement;

    private Vector2 _lastVelocity;

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    public override void Initialize() {
        grappleViewRef.Set(this);

        StartActing();

        _charAccess.ControlVelocity.StopDirectionalMovement();

        _grapplingHook.StoppedGrappleLocking += StopGrapplingMidAir;
    }

    public override void Dispose() {
       
        _grapplingHook.StoppedGrappleLocking -= StopGrapplingMidAir;

        _grapplingHook.ExitGrappleLock();

        Destroy(true);
    }

    public void StopGrapplingMidAir() {

        _charAccess.ControlVelocity.SetDirection(_charAccess.ControlVelocity.GetVelocityDirection());

        _charAccess.ControlSpeed.TempSpeedIncrease();

        if (StopTrigger != null)
            StopTrigger();

        activateFloatingStateEvent.Dispatch();
    }

    private void Update() {
        if (_charAccess.ControlVelocity.GetVelocity == Vector2.zero && _charAccess.ControlVelocity.CurrentSpeed != 0) {
            _charAccess.ControlDirection.ApplyLogicDirection(_charAccess.ControlVelocity.GetVelocityDirection(), _charAccess.CollisionDirection.GetCurrentCollDir());

            activateSlidingStateEvent.Dispatch();
        }

        _gunLookAt.UpdateLookAt(_grapplingHook.Destination);
    }

    private void FixedUpdate() {
        _charAccess.ControlVelocity.SetVelocity(_lastVelocity = _charAccess.ControlVelocity.GetVelocityDirection() * _charAccess.ControlVelocity.CurrentSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (_charAccess.ControlTakeOff.CheckToBounce(collision)) {
            _charAccess.ControlTakeOff.Bounce(_lastVelocity.normalized, _charAccess.CollisionDirection.GetUpdatedCollDir(collision));
            activateFloatingStateEvent.Dispatch();
        } else {
            _charAccess.ControlDirection.ApplyLogicDirection(_lastVelocity.normalized, _charAccess.CollisionDirection.GetUpdatedCollDir(collision));
            activateSlidingStateEvent.Dispatch();
        }
    }
}
