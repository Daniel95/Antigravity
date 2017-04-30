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

    public void EnterGrappleLock() {
        //if (StopTrigger != null)
        //{
        //    StopTrigger();
        //}

        StartDistaneJoint(distance);

        grappleAnchorUpdate = StartCoroutine(GrappleAnchorUpdate());
    }

    public void ExitGrappleLock()
    {
        Canceled();
    }

    public void Canceled() {
        distanceJoint.enabled = false;

        if (grappleAnchorUpdate != null) {
           StopCoroutine(grappleAnchorUpdate);
        }
    }

    private IEnumerator GrappleAnchorUpdate() {
        while (true) {
            updateGrapplingHookRopeEvent.Dispatch();
            yield return null;
        }
    }

    private void DeactivateHook() {
        StopDistanceJoint();
    }

    private void StartDistaneJoint(float distance) {
         //init the distance joint & line renderer
        distanceJoint.enabled = true;

        distanceJoint.connectedAnchor = hookRef.Get().Anchors[0].position;
        distanceJoint.distance = distance;
    }

    public void StopDistanceJoint() {
        if (grappleAnchorUpdate != null)
            StopCoroutine(grappleAnchorUpdate);

        distanceJoint.enabled = false;
    }

}
 