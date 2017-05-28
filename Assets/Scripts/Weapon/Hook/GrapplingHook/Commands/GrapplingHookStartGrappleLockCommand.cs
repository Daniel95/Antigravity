using IoCPlus;
using UnityEngine;

public class GrapplingHookStartGrappleLockCommand : Command {

    [Inject] private Ref<IGrapplingHook> grapplingHookRef;
    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        grapplingHookRef.Get().DistanceJoint.connectedAnchor = hookRef.Get().Anchors[0].position;
        grapplingHookRef.Get().StartGrappleLock();
    }
}
