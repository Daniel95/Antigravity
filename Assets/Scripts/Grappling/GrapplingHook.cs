using UnityEngine;
using System;
using System.Collections;

public class GrapplingHook : MonoBehaviour, IWeapon, ITriggerer {

    [SerializeField]
    private GameObject grappleProjectilePrefab;

    [SerializeField]
    private float minDistance = 0.3f;

    //variables to control the unity components
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint;

    //to save our LineUpdateCoroutine;
    private Coroutine lineUpdateCoroutine;
    private Coroutine holdGrappleCoroutine;

    private Coroutine grappleAnchorUpdate;

    //other scripts can subscribe to know when the grapple locks and unlocks
    public Action startedGrappleLocking;
    public Action stoppedGrappleLocking;

    //our current grapple data
    private GameObject grappleProjectileGObj;
    private GrappleProjectile grappleProjectileScript;

    private enum GrapplingHookStates { busyShooting, busyHolding, busyPullingBack, activate, inactive };

    private GrapplingHookStates currentGrapplingHookState = GrapplingHookStates.inactive;

    private AimRay aimRay;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    void Start() {

        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;

        lineRenderer = GetComponent<LineRenderer>();

        aimRay = GetComponent<AimRay>();

        grappleProjectileGObj = Instantiate(grappleProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        grappleProjectileScript = grappleProjectileGObj.GetComponent<GrappleProjectile>();
        grappleProjectileGObj.SetActive(false);
    }

    public void Dragging(Vector2 _destination, Vector2 _spawnPosition) {
        if (!aimRay.AimRayActive) {
            aimRay.StartAimRay(_destination);
        }

        aimRay.SetRayDestination = _destination;
    }

    //cancel aiming
    public void CancelDragging()
    {
        StopBulletTime();
    }

    void StopBulletTime() {
        aimRay.StopAimRay();
    }

    //spawns the grapple projectile and activates its moveTowards script
    public void Release(Vector2 _destination, Vector2 _spawnPosition) {
        StopBulletTime();

        if (currentGrapplingHookState == GrapplingHookStates.inactive)
        {
            ShootGrappleHook(_destination, _spawnPosition);
        }
        //if we still have a grapple activate, deactivate it first before we shoot a new one
        else if (currentGrapplingHookState == GrapplingHookStates.activate || currentGrapplingHookState == GrapplingHookStates.busyShooting)
        {
            holdGrappleCoroutine = StartCoroutine(HoldGrapple( _destination, _spawnPosition));
            PullBack();
        } 
    }

    IEnumerator HoldGrapple(Vector2 _destination, Vector2 _spawnPosition) {
        while (currentGrapplingHookState != GrapplingHookStates.inactive) {
            yield return null;
        }

        ShootGrappleHook(_destination, _spawnPosition);
    }

    private void ShootGrappleHook(Vector2 _destination, Vector2 _spawnPosition) {
        currentGrapplingHookState = GrapplingHookStates.busyShooting;

        grappleProjectileGObj.SetActive(true);
        grappleProjectileGObj.transform.position = _spawnPosition;

        //activate line renderer
        lineRenderer.enabled = true;
        lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());

        grappleProjectileScript.reachedDestination = EnterGrappleLock;
        grappleProjectileScript.grappleCanceled = ExitGrappleLock;
        grappleProjectileScript.GoToShootPos(_destination);
    }

    IEnumerator UpdateLineRendererPositions() {
        while (true) {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grappleProjectileGObj.transform.position);
            yield return null;
        }
    }

    private void EnterGrappleLock()
    {
        float distance = Vector2.Distance(transform.position, grappleProjectileGObj.transform.position);
        //only start grappleLocking when the distance to the player is above the minimal distance
        if (distance > minDistance)
        {
            currentGrapplingHookState = GrapplingHookStates.activate;

            grappleProjectileScript.reachedDestination -= EnterGrappleLock;

            if (stopTrigger != null)
            {
                stopTrigger();
            }

            //activate distanceJoint2D
            if (startedGrappleLocking != null)
            {
                startedGrappleLocking();
            }

            distanceJoint.enabled = true;

            distanceJoint.connectedAnchor = grappleProjectileGObj.transform.position;
            distanceJoint.distance = distance;

            //keep updating the grapple anchor if the object we are attached to is MoveAble
            if (grappleProjectileScript.CollidedWithMoveAble())
            {
                grappleAnchorUpdate = StartCoroutine(GrappleAnchorUpdate());
            }
        }
        else {
            //else we exit the grapplehook and pull it back to the player
            ExitGrappleLock();
        }
    }

    IEnumerator GrappleAnchorUpdate()
    {
        while(true)
        {
            distanceJoint.connectedAnchor = grappleProjectileGObj.transform.position;
            yield return new WaitForFixedUpdate();
        }
    }

    //used to cancel grapple locking if we are currently locked, also cancels the grapplingstate
    public void ExitGrappleLock() {

        if (stoppedGrappleLocking != null)
            stoppedGrappleLocking();

        grappleProjectileScript.reachedDestination -= EnterGrappleLock;
        distanceJoint.enabled = false;

        if (holdGrappleCoroutine != null)
            StopCoroutine(holdGrappleCoroutine);

        if (grappleAnchorUpdate != null)
            StopCoroutine(grappleAnchorUpdate);

        PullBack();
    }

    //pulls the grappling hook back to the player, once it reached the player set in inactive it
    void PullBack() {
        //only pullback when we aren't already pulling back and the we are not in the inactive state. 
        if (currentGrapplingHookState != GrapplingHookStates.busyPullingBack && currentGrapplingHookState != GrapplingHookStates.inactive)
        {
            currentGrapplingHookState = GrapplingHookStates.busyPullingBack;

            grappleProjectileScript.reachedDestination = DonePullingBack;
            grappleProjectileScript.Return(transform.position);
        }
    }

    void DonePullingBack() {
        grappleProjectileScript.reachedDestination -= DonePullingBack;
        DestroyGrappleLock();
    }

    void DestroyGrappleLock()
    {
        currentGrapplingHookState = GrapplingHookStates.inactive;
        StopLineRenderer();
        grappleProjectileGObj.SetActive(false);
    }

    void StopLineRenderer() {
        lineRenderer.enabled = false;
        StopCoroutine(lineUpdateCoroutine);
    }

    public Vector2 Destination
    {
        get { return grappleProjectileGObj.transform.position; }
    }
}
