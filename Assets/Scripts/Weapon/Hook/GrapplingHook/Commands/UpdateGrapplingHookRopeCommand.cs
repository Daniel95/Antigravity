using IoCPlus;
using UnityEngine;

public class UpdateGrapplingHookRopeCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    [Inject] private AddHookAnchorEvent addHookAnchorEvent;

    /// <summary>
    /// Check for collisions and update the anchor position of the grappling hook line.
    /// </summary>
    /// <returns></returns>
    protected override void Execute() {
        IHook hook = hookRef.Get();
        IGrapplingHook grapplingHook = grapplingHookRef.Get();
        Vector2 ownerPosition = hook.Owner.transform.position;

        RaycastHit2D hitToAnchor = Physics2D.Linecast(ownerPosition, hook.Anchors[hook.Anchors.Count - 1].position);

        if (hitToAnchor.collider != null) {
            Vector2 anchorPosition = hitToAnchor.point + (ownerPosition - hitToAnchor.point).normalized * 0.1f;
            addHookAnchorEvent.Dispatch(anchorPosition, hitToAnchor.transform);
            grapplingHook.DistanceJoint.distance = Vector2.Distance(ownerPosition, hitToAnchor.point);
        } else if (hook.Anchors.Count > 1) {
            RaycastHit2D hitToPreviousAnchor = Physics2D.Linecast(ownerPosition, hook.Anchors[hook.Anchors.Count - 2].position);
            if (hitToPreviousAnchor.collider == null) {
                grapplingHook.DistanceJoint.distance = Vector2.Distance(ownerPosition, hook.Anchors[hook.Anchors.Count - 1].position) + Vector2.Distance(hook.Anchors[hook.Anchors.Count - 1].position, hook.Anchors[hook.Anchors.Count - 2].position);
                hook.Anchors.RemoveAt(hook.Anchors.Count - 1);
                hook.LineRenderer.positionCount--;
            }
        }

        grapplingHook.DistanceJoint.connectedAnchor = hook.Anchors[hook.Anchors.Count - 1].position;
    }
}
