using IoCPlus;
using System;
using UnityEngine;

public class GrapplingStateView : View, IGrapplingState, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<IGrapplingState> grappleViewRef;

    [Inject] private CharacterEnableDirectionalMovementEvent characterEnableDirectionalMovementEvent;

    [Inject] private ActivateFloatingStateEvent activateFloatingStateEvent;
    [Inject] private ActivateSlidingStateEvent activateSlidingStateEvent;

    [SerializeField] private GameObject gun;

    private LookAt gunLookAt;
    private Coroutine slingMovement;
    private Vector2 lastVelocity;

    private void Awake() {
        gunLookAt = gun.GetComponent<LookAt>();
    }

    public override void Initialize() {
        grappleViewRef.Set(this);

        StartFixedActing();
        StartActing();
    }

    public override void Dispose() {
        Delete();
    }

    public override void Act() {
        if (charAcces.ControlVelocity.GetVelocity == Vector2.zero && charAcces.ControlVelocity.CurrentSpeed != 0) {
            charAcces.ControlDirection.ApplyLogicDirection(charAcces.ControlVelocity.GetVelocityDirection(), charAcces.CollisionDirection.GetCurrentCollDir());

            activateSlidingStateEvent.Dispatch();
        }

        gunLookAt.UpdateLookAt(_grapplingHook.Destination);
    }

    public override void FixedAct() {
        charAcces.ControlVelocity.SetVelocity(lastVelocity = charAcces.ControlVelocity.GetVelocityDirection() * charAcces.ControlVelocity.CurrentSpeed);
    }
}
