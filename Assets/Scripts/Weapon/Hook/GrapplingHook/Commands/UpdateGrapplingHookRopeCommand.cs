using IoCPlus;
using UnityEngine;

public class UpdateGrapplingHookRopeCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    [Inject] private AddHookAnchorEvent addHookAnchorEvent;

    protected override void Execute() {
        IHook hook = hookRef.Get();
        IGrapplingHook grapplingHook = grapplingHookRef.Get();
        Vector2 ownerPosition = hook.Owner.transform.position;

        RaycastHit2D hitToAnchor = Physics2D.Linecast(ownerPosition, hook.Anchors[0].position);

        if (hitToAnchor.collider != null) {
            Vector2 anchorPosition = hitToAnchor.point + (ownerPosition - hitToAnchor.point).normalized * 0.1f;
            addHookAnchorEvent.Dispatch(anchorPosition, hitToAnchor.transform);
            grapplingHook.DistanceJoint.distance = Vector2.Distance(ownerPosition, hitToAnchor.point);
        } else if (hook.Anchors.Count > 1) {
            RaycastHit2D hitToPreviousAnchor = Physics2D.Linecast(ownerPosition, hook.Anchors[1].position);
            if (hitToPreviousAnchor.collider == null) {
                grapplingHook.DistanceJoint.distance = Vector2.Distance(ownerPosition, hook.Anchors[0].position) + Vector2.Distance(hook.Anchors[0].position, hook.Anchors[1].position);
                hook.DestroyAnchorAt(0);
            }
        }

        grapplingHook.DistanceJoint.connectedAnchor = hook.Anchors[0].position;
    }
}
