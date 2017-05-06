using IoCPlus;
using System;
using UnityEngine;

public class GrapplingStateView : View, IGrapplingState, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<IGrapplingState> grapplingStateRef;

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
        grapplingStateRef.Set(this);
    }

    public override void Dispose() {
        Delete();
    }


}
