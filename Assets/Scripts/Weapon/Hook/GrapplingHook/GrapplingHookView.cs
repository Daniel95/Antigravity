using IoCPlus;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
public class GrapplingHookView : View, IGrapplingHook {

    public float HookDistance { get { return distance; } set { distance = value; } }
    public DistanceJoint2D DistanceJoint { get { return distanceJoint; } set { distanceJoint = value; } }

    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    [Inject] private UpdateGrapplingHookRopeEvent updateGrapplingHookRopeEvent;

    private DistanceJoint2D distanceJoint;
    private Coroutine grappleAnchorUpdateCoroutine;
    private float distance;

    private void Awake() {
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
    }

    public override void Initialize() {
        grapplingHookRef.Set(this);
    }

    public void StartGrappleLock() {
        Debug.Log("StartGrappleLock f." + Time.frameCount);
        distanceJoint.enabled = true;
        distanceJoint.distance = distance;

        grappleAnchorUpdateCoroutine = StartCoroutine(GrappleAnchorUpdate());
    }

    public void StopGrappleLock() {
        Debug.Log("StopGrappleLock f." + Time.frameCount);
        if (grappleAnchorUpdateCoroutine != null) {
            StopCoroutine(grappleAnchorUpdateCoroutine);
            grappleAnchorUpdateCoroutine = null;
        }

        distanceJoint.enabled = false;
    }

    private IEnumerator GrappleAnchorUpdate() {
        while (true) {
            Debug.Log("GrappleAnchorUpdate f." + Time.frameCount);
            updateGrapplingHookRopeEvent.Dispatch();
            yield return null;
        }
    }
}
 