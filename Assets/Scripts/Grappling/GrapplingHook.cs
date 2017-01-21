using UnityEngine;
using System;
using System.Collections;
using Boo.Lang;

public class GrapplingHook : MonoBehaviour, IWeapon, ITriggerer {

    [SerializeField]
    private LayerMask rayLayers;

    [SerializeField]
    private GameObject grappleProjectilePrefab;

    [SerializeField]
    private float minDistance = 0.3f;

    [SerializeField]
    private float directionSpeedNeutralValue = 0.3f;

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

    private CharScriptAccess _charAcces;

    private enum GrapplingHookStates { BusyShooting, BusyPullingBack, Active, Inactive };

    private GrapplingHookStates _currentGrapplingHookState = GrapplingHookStates.Inactive;

    private AimRay _aimRay;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    private readonly List<Transform> _anchors = new List<Transform>();

    void Start() {

        _distanceJoint = GetComponent<DistanceJoint2D>();
        _distanceJoint.enabled = false;

        _lineRenderer = GetComponent<LineRenderer>();

        _aimRay = GetComponent<AimRay>();

        _charAcces = GetComponent<CharScriptAccess>();

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
        else if (_currentGrapplingHookState == GrapplingHookStates.Active || _currentGrapplingHookState == GrapplingHookStates.BusyShooting)
        {
            _holdGrappleCoroutine = StartCoroutine(HoldGrapple( destination, spawnPosition));
            StopDistanceJoint();
            PullBack();
        } 
    }

    private IEnumerator HoldGrapple(Vector2 destination, Vector2 spawnPosition) {
        while (_currentGrapplingHookState != GrapplingHookStates.Inactive) {
            yield return null;
        }

        ShootGrappleHook(destination, spawnPosition);
    }

    private void ShootGrappleHook(Vector2 destination, Vector2 spawnPosition) {
        _currentGrapplingHookState = GrapplingHookStates.BusyShooting;

        _grappleProjectileGObj.SetActive(true);
        _grappleProjectileGObj.transform.position = spawnPosition;

        Vector2 anchorPos = _grappleProjectileGObj.transform.position + (_grappleProjectileGObj.transform.position - transform.position).normalized * 0.1f;
        _anchors.Add(CreateGrappleAnchor(anchorPos, _grappleProjectileGObj.transform));

        //activate line renderer
        _lineRenderer.enabled = true;

        _lineRenderer.numPositions = 2;
        _lineRenderer.SetPosition(0, _anchors[0].position);
        _lineRenderer.SetPosition(1, transform.position);

        _lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());

        _grappleProjectileScript.ReachedDestination = EnterGrappleLock;
        _grappleProjectileScript.GrappleCanceled = ExitGrappleLock;
        _grappleProjectileScript.GoToShootPos(destination);
    }

    private void EnterGrappleLock()
    {
        float distance = Vector2.Distance(transform.position, _grappleProjectileGObj.transform.position);
        //only start grappleLocking when the distance to the player is above the minimal distance
        if (distance > minDistance)
        {
            _currentGrapplingHookState = GrapplingHookStates.Active;

            _grappleProjectileScript.ReachedDestination = null;

            if (StopTrigger != null)
            {
                StopTrigger();
            }

            //activate distanceJoint2D
            if (StartedGrappleLocking != null)
            {
                StartedGrappleLocking();
            }

            //init the distance joint & line renderer
            _distanceJoint.enabled = true;

            _distanceJoint.connectedAnchor = _anchors[0].position;
            _distanceJoint.distance = distance;

            //check for collisions and update the anchor position of the grappling hook line
            _grappleAnchorUpdate = StartCoroutine(GrappleAnchorUpdate());


            //change speed by calculating the angle
            float angleDifference =
                Mathf.Abs(Vector2.Dot((_grappleProjectileGObj.transform.position - transform.position).normalized,
                    _charAcces.ControlVelocity.GetVelocityDirection()));

            float speedChange = angleDifference * -1 + 1;

            _charAcces.ControlSpeed.TempSpeedChange(speedChange, directionSpeedNeutralValue);
        }
        else {
            //else we exit the grapplehook and pull it back to the player
            ExitGrappleLock();
        }
    }

    private IEnumerator GrappleAnchorUpdate()
    {
        while (true)
        {
            RaycastHit2D hitToAnchor = Physics2D.Linecast(transform.position, _anchors[_anchors.Count - 1].position, rayLayers);

            if (hitToAnchor.collider != null)
            {
                Vector2 anchorPos = hitToAnchor.point + ((Vector2)transform.position - hitToAnchor.point).normalized * 0.1f;

                _anchors.Add(CreateGrappleAnchor(anchorPos, hitToAnchor.transform));

                _lineRenderer.numPositions = _anchors.Count + 1;

                _distanceJoint.distance = Vector2.Distance(transform.position, hitToAnchor.point);
            }
            else if (_anchors.Count > 1)
            {

                RaycastHit2D hitToPreviousAnchor = Physics2D.Linecast(transform.position, _anchors[_anchors.Count - 2].position, rayLayers);

                if (hitToPreviousAnchor.collider == null)
                {

                    _distanceJoint.distance = Vector2.Distance(transform.position, _anchors[_anchors.Count - 1].position) + Vector2.Distance(_anchors[_anchors.Count - 1].position, _anchors[_anchors.Count - 2].position);

                    _anchors.RemoveAt(_anchors.Count - 1);

                    _lineRenderer.numPositions--;
                }
            }

            //update the pos each frame
            _distanceJoint.connectedAnchor = _anchors[_anchors.Count - 1].position;

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator UpdateLineRendererPositions()
    {
        while (true)
        {
            for (int i = 0; i < _anchors.Count; i++)
            {
                _lineRenderer.SetPosition(i, _anchors[i].position);
            }

            _lineRenderer.SetPosition(_lineRenderer.numPositions - 1, transform.position);
            yield return null;
        }
    }

    //used to cancel grapple locking if we are currently locked, also cancels the grapplingstate
    public void ExitGrappleLock() {
        if (StoppedGrappleLocking != null)
            StoppedGrappleLocking();

        _grappleProjectileScript.ReachedDestination = null;
        _distanceJoint.enabled = false;

        if (_holdGrappleCoroutine != null)
            StopCoroutine(_holdGrappleCoroutine);

        if (_grappleAnchorUpdate != null)
           StopCoroutine(_grappleAnchorUpdate);

        PullBack();
    }

    //pulls the grappling hook back to the player, once it reached the player set it to inactive
    private void PullBack()
    {

        //only pullback when we aren't already pulling back and the we are not in the inactive state. 
        if (_currentGrapplingHookState != GrapplingHookStates.BusyPullingBack && _currentGrapplingHookState != GrapplingHookStates.Inactive)
        {
            _currentGrapplingHookState = GrapplingHookStates.BusyPullingBack;

            _grappleProjectileScript.ReachedDestination = DeactivateGrappleLock;
            _grappleProjectileScript.Return(transform.position);
        }
    }

    private void DeactivateGrappleLock() {

        _grappleProjectileScript.ReachedDestination = null;
        _currentGrapplingHookState = GrapplingHookStates.Inactive;

        _grappleProjectileGObj.SetActive(false);

        StopLineRenderer();
        StopDistanceJoint();
    }

    private void StopLineRenderer() {
        _lineRenderer.enabled = false;
        StopCoroutine(_lineUpdateCoroutine);

        _lineRenderer.numPositions = 0;
    }

    public void StopDistanceJoint()
    {

        if (_grappleAnchorUpdate != null)
            StopCoroutine(_grappleAnchorUpdate);

        _distanceJoint.enabled = false;

        for (int i = 0; i < _anchors.Count; i++)
        {
            Destroy(_anchors[i].gameObject);
        }
        _anchors.Clear();
    }

    public Vector2 Destination
    {
        get { return _grappleProjectileGObj.transform.position; }
    }

    private Transform CreateGrappleAnchor(Vector2 pos, Transform parent)
    {
        var p = new GameObject("GrappleAnchor");
        p.transform.position = pos;
        p.layer = LayerMask.NameToLayer("Ignore Raycast");
        p.transform.SetParent(parent);
        return p.transform;
    }
}
