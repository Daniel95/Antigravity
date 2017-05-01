using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
public class GrapplingHookView : View, IGrapplingHook {

    public float HookDistance { get { return distance; } set { distance = value; } }
    public DistanceJoint2D DistanceJoint { get { return distanceJoint; } set { distanceJoint = value; } }

    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    [Inject] private UpdateGrapplingHookRopeEvent updateGrapplingHookRopeEvent;

    private DistanceJoint2D distanceJoint;
    private Coroutine grappleAnchorUpdate;
    private float distance;

    private void Awake() {
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
    }

    public override void Initialize() {
        grapplingHookRef.Set(this);
    }

    public void StartDistaneJoint() {
        distanceJoint.enabled = true;
        distanceJoint.distance = distance;

        grappleAnchorUpdate = StartCoroutine(GrappleAnchorUpdate());
    }

    public void StopDistanceJoint() {
        if (grappleAnchorUpdate != null) {
            StopCoroutine(grappleAnchorUpdate);
        }

        distanceJoint.enabled = false;
    }

    private IEnumerator GrappleAnchorUpdate() {
        while (true) {
            updateGrapplingHookRopeEvent.Dispatch();
            yield return null;
        }
    }
}
 