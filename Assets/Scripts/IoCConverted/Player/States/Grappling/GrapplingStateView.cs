using IoCPlus;
using System;
using UnityEngine;

public class GrapplingStateView : View, IGrapplingState, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<IGrapplingState> grapplingStateRef;

    private Coroutine slingMovement;
    private Vector2 lastVelocity;

    public override void Initialize() {
        grapplingStateRef.Set(this);
    }

    public override void Dispose() {
        Delete();
    }
}
