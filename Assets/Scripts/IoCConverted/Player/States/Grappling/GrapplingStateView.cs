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

    private CharScriptAccess charAcces;

    private LookAt _gunLookAt;

    private Coroutine _slingMovement;

    private Vector2 _lastVelocity;

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    private void Awake() {
        charAcces = GetComponent<CharScriptAccess>();
    }

    public override void Initialize() {
        grappleViewRef.Set(this);

        StartFixedActing();
        StartActing();

        charAcces.ControlVelocity.StopDirectionalMovement();

        _grapplingHook.StoppedGrappleLocking += StopGrapplingMidAir;
    }

    public override void Dispose() {
       
        _grapplingHook.StoppedGrappleLocking -= StopGrapplingMidAir;

        _grapplingHook.ExitGrappleLock();

        Delete();
    }

    public void StopGrapplingMidAir() {

        charAcces.ControlVelocity.SetDirection(charAcces.ControlVelocity.GetVelocityDirection());

        charAcces.ControlSpeed.TempSpeedIncrease();

        if (StopTrigger != null)
            StopTrigger();

        activateFloatingStateEvent.Dispatch();
    }

    public override void Act() {
        if (charAcces.ControlVelocity.GetVelocity == Vector2.zero && charAcces.ControlVelocity.CurrentSpeed != 0) {
            charAcces.ControlDirection.ApplyLogicDirection(charAcces.ControlVelocity.GetVelocityDirection(), charAcces.CollisionDirection.GetCurrentCollDir());

            activateSlidingStateEvent.Dispatch();
        }

        _gunLookAt.UpdateLookAt(_grapplingHook.Destination);
    }

    public override void FixedAct() {
        charAcces.ControlVelocity.SetVelocity(_lastVelocity = charAcces.ControlVelocity.GetVelocityDirection() * charAcces.ControlVelocity.CurrentSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (charAcces.ControlTakeOff.CheckToBounce(collision)) {
            charAcces.ControlTakeOff.Bounce(_lastVelocity.normalized, charAcces.CollisionDirection.GetUpdatedCollDir(collision));
            activateFloatingStateEvent.Dispatch();
        } else {
            charAcces.ControlDirection.ApplyLogicDirection(_lastVelocity.normalized, charAcces.CollisionDirection.GetUpdatedCollDir(collision));
            activateSlidingStateEvent.Dispatch();
        }
    }
}
