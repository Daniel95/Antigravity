using UnityEngine;
using System;
using System.Collections;

public class GrapplingHook : MonoBehaviour, IWeapon, ITriggerer {

    [SerializeField]
    private GameObject grappleProjectilePrefab;

    [SerializeField]
    private float minDistance = 0.3f;

    //variables to control the unity components
    private LineRenderer _lineRenderer;
    private DistanceJoint2D _distanceJoint;

    //to save our LineUpdateCoroutine;
    private Coroutine _lineUpdateCoroutine;
    private Coroutine _holdGrappleCoroutine;

    private Coroutine _grappleAnchorUpdate;

    //other scripts can subscribe to know when the grapple locks and unlocks
    public Action StartedGrappleLocking;
    public Action StoppedGrappleLocking;

    //our current grapple data
    private GameObject _grappleProjectileGObj;
    private GrappleProjectile _grappleProjectileScript;

    private enum GrapplingHookStates { BusyShooting, BusyHolding, BusyPullingBack, Activate, Inactive };

    private GrapplingHookStates _currentGrapplingHookState = GrapplingHookStates.Inactive;

    private AimRay _aimRay;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    void Start() {

        _distanceJoint = GetComponent<DistanceJoint2D>();
        _distanceJoint.enabled = false;

        _lineRenderer = GetComponent<LineRenderer>();

        _aimRay = GetComponent<AimRay>();

        _grappleProjectileGObj = Instantiate(grappleProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        _grappleProjectileScript = _grappleProjectileGObj.GetComponent<GrappleProjectile>();
        _grappleProjectileGObj.SetActive(false);
    }

    public void Dragging(Vector2 destination, Vector2 spawnPosition) {
        if (!_aimRay.AimRayActive) {
            _aimRay.StartAimRay(destination);
        }

        _aimRay.RayDestination = destination;
    }

    /// <summary>
    /// cancel aiming
    /// </summary>
    public void CancelDragging()
    {
        StopBulletTime();
    }

    private void StopBulletTime() {
        _aimRay.StopAimRay();
    }

    /// <summary>
    /// spawns the grapple projectile and activates its moveTowards script
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="spawnPosition"></param>
    public void Release(Vector2 destination, Vector2 spawnPosition) {
        StopBulletTime();

        if (_currentGrapplingHookState == GrapplingHookStates.Inactive)
        {
            ShootGrappleHook(destination, spawnPosition);
        }
        //if we still have a grapple activate, deactivate it first before we shoot a new one
        else if (_currentGrapplingHookState == GrapplingHookStates.Activate || _currentGrapplingHookState == GrapplingHookStates.BusyShooting)
        {
            _holdGrappleCoroutine = StartCoroutine(HoldGrapple( destination, spawnPosition));
            PullBack();
        } 
    }

    IEnumerator HoldGrapple(Vector2 destination, Vector2 spawnPosition) {
        while (_currentGrapplingHookState != GrapplingHookStates.Inactive) {
            yield return null;
        }

        ShootGrappleHook(destination, spawnPosition);
    }

    private void ShootGrappleHook(Vector2 destination, Vector2 spawnPosition) {
        _currentGrapplingHookState = GrapplingHookStates.BusyShooting;

        _grappleProjectileGObj.SetActive(true);
        _grappleProjectileGObj.transform.position = spawnPosition;

        //activate line renderer
        _lineRenderer.enabled = true;
        _lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());

        _grappleProjectileScript.ReachedDestination = EnterGrappleLock;
        _grappleProjectileScript.GrappleCanceled = ExitGrappleLock;
        _grappleProjectileScript.GoToShootPos(destination);
    }

    IEnumerator UpdateLineRendererPositions() {
        while (true) {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _grappleProjectileGObj.transform.position);
            yield return null;
        }
    }

    private void EnterGrappleLock()
    {
        float distance = Vector2.Distance(transform.position, _grappleProjectileGObj.transform.position);
        //only start grappleLocking when the distance to the player is above the minimal distance
        if (distance > minDistance)
        {
            _currentGrapplingHookState = GrapplingHookStates.Activate;

            _grappleProjectileScript.ReachedDestination -= EnterGrappleLock;

            if (StopTrigger != null)
            {
                StopTrigger();
            }

            //activate distanceJoint2D
            if (StartedGrappleLocking != null)
            {
                StartedGrappleLocking();
            }

            _distanceJoint.enabled = true;

            _distanceJoint.connectedAnchor = _grappleProjectileGObj.transform.position;
            _distanceJoint.distance = distance;

            //keep updating the grapple anchor if the object we are attached to is MoveAble
            if (_grappleProjectileScript.CollidedWithMoveAble())
            {
                _grappleAnchorUpdate = StartCoroutine(GrappleAnchorUpdate());
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
            _distanceJoint.connectedAnchor = _grappleProjectileGObj.transform.position;
            yield return new WaitForFixedUpdate();
        }
    }

    //used to cancel grapple locking if we are currently locked, also cancels the grapplingstate
    public void ExitGrappleLock() {

        if (StoppedGrappleLocking != null)
            StoppedGrappleLocking();

        _grappleProjectileScript.ReachedDestination -= EnterGrappleLock;
        _distanceJoint.enabled = false;

        if (_holdGrappleCoroutine != null)
            StopCoroutine(_holdGrappleCoroutine);

        if (_grappleAnchorUpdate != null)
            StopCoroutine(_grappleAnchorUpdate);

        PullBack();
    }

    //pulls the grappling hook back to the player, once it reached the player set in inactive it
    void PullBack() {
        //only pullback when we aren't already pulling back and the we are not in the inactive state. 
        if (_currentGrapplingHookState != GrapplingHookStates.BusyPullingBack && _currentGrapplingHookState != GrapplingHookStates.Inactive)
        {
            _currentGrapplingHookState = GrapplingHookStates.BusyPullingBack;

            _grappleProjectileScript.ReachedDestination = DonePullingBack;
            _grappleProjectileScript.Return(transform.position);
        }
    }

    void DonePullingBack() {
        _grappleProjectileScript.ReachedDestination -= DonePullingBack;
        DestroyGrappleLock();
    }

    void DestroyGrappleLock()
    {
        _currentGrapplingHookState = GrapplingHookStates.Inactive;
        StopLineRenderer();
        _grappleProjectileGObj.SetActive(false);
    }

    private void StopLineRenderer() {
        _lineRenderer.enabled = false;
        StopCoroutine(_lineUpdateCoroutine);
    }

    public Vector2 Destination
    {
        get { return _grappleProjectileGObj.transform.position; }
    }
}
