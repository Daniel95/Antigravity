using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HookWeapon : MonoBehaviour, IWeaponOutput, ITriggerer {

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    public Vector2 Destination
    {
        get { return HookProjectileGObj.transform.position; }
    }

    [SerializeField]
    protected LayerMask RayLayers;

    [SerializeField]
    private GameObject hookProjectilePrefab;

    [SerializeField]
    private List<HookTypeBase> hookTypes;

    [SerializeField]
    private float directionSpeedNeutralValue = 0.15f;

    protected HookStates CurrentHookState = HookStates.Inactive;

    //variables to control the unity components
    protected LineRenderer LineRenderer;

    //our current grapple data
    protected GameObject HookProjectileGObj;
    protected HookProjectile HookProjectileScript;

    protected readonly List<Transform> Anchors = new List<Transform>();

    //to save our LineUpdateCoroutine;
    private Coroutine _lineUpdateCoroutine;
    private Coroutine _holdGrappleCoroutine;

    private CharScriptAccess _charAcces;

    protected enum HookStates { BusyShooting, BusyPullingBack, Active, Inactive }

    private AimLineView _aimRay;

    public static class HookAbleLayers
    {
        public static int GrappleSurface =  LayerMask.NameToLayer("GrappleHook");
        public static int PullSurface =  LayerMask.NameToLayer("PullHook");
    }

    public void Aiming(Vector2 destination, Vector2 spawnPosition) {
        if (!_aimRay.AimLineActive) {
            _aimRay.StartAimLine(destination);
        }

        _aimRay.LineDestination = destination;
    }

    /// <summary>
    /// cancel aiming
    /// </summary>
    public void CancelAiming() {
        StopBulletTime();
    }

    private void StopBulletTime() {
        _aimRay.StopAimLine();
    }

    /// <summary>
    /// spawns the grapple projectile and activates its moveTowards script
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="spawnPosition"></param>
    public void Fire(Vector2 destination, Vector2 spawnPosition) {
        StopBulletTime();

        if (CurrentHookState == HookStates.Inactive)
        {
            print("shoot : " + this);
            ShootHook(destination, spawnPosition);
        }
        //if we still have a grapple activate, deactivate it first before we shoot a new one
        else if (CurrentHookState == HookStates.Active || CurrentHookState == HookStates.BusyShooting)
        {
            _holdGrappleCoroutine = StartCoroutine(HoldGrapple( destination, spawnPosition));
            PullBack();
        } 
    }

    protected virtual void Awake() {
        LineRenderer = GetComponent<LineRenderer>();

        _aimRay = GetComponent<AimLineView>();

        _charAcces = GetComponent<CharScriptAccess>();

        HookProjectileGObj = Instantiate(hookProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
        HookProjectileScript = HookProjectileGObj.GetComponent<HookProjectile>();
        HookProjectileGObj.SetActive(false);
    }

    protected virtual void Hooked(int hookedLayer) { }

    protected virtual void Canceled()
    {
        HookProjectileScript.Attached = null;

        if (_holdGrappleCoroutine != null)
            StopCoroutine(_holdGrappleCoroutine);

        PullBack();
    }

    protected void ChangeSpeedByAngle()
    {
        //change speed by calculating the angle
        float angleDifference =
            Mathf.Abs(Vector2.Dot((HookProjectileGObj.transform.position - transform.position).normalized,
                _charAcces.ControlVelocity.GetVelocityDirection()));

        float speedChange = angleDifference * -1 + 1;

        _charAcces.ControlSpeed.TempSpeedChange(speedChange, directionSpeedNeutralValue);
    }

    protected Transform CreateGrappleAnchor(Vector2 pos, Transform parent)
    {
        GameObject p = new GameObject("GrappleAnchor");
        p.transform.position = pos;
        p.layer = LayerMask.NameToLayer("Ignore Raycast");
        p.transform.SetParent(parent);
        return p.transform;
    }

    protected virtual void DeactivateHook() {
        HookProjectileScript.Returned = null;
        CurrentHookState = HookStates.Inactive;

        HookProjectileGObj.SetActive(false);

        print("DestroyAnchors");
        DestroyAnchors();
        print("StopLinerenderer");
        StopLineRenderer();
    }

    private IEnumerator HoldGrapple(Vector2 destination, Vector2 spawnPosition) {
        while (CurrentHookState != HookStates.Inactive) {
            yield return null;
        }

            print("shoot");


        ShootHook(destination, spawnPosition);
    }

    private void ShootHook(Vector2 destination, Vector2 spawnPosition) {
        CurrentHookState = HookStates.BusyShooting;

        HookProjectileGObj.SetActive(true);
        HookProjectileGObj.transform.position = spawnPosition;

        Vector2 anchorPos = HookProjectileGObj.transform.position + (HookProjectileGObj.transform.position - transform.position).normalized * 0.1f;
        Anchors.Add(CreateGrappleAnchor(anchorPos, HookProjectileGObj.transform));

        //activate line renderer
        LineRenderer.enabled = true;

        LineRenderer.positionCount = 2;
        LineRenderer.SetPosition(0, Anchors[0].position);
        LineRenderer.SetPosition(1, transform.position);

        print("Start coroutine");
        _lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());

        HookProjectileScript.Attached = Hooked;
        HookProjectileScript.Canceled = Canceled;
        HookProjectileScript.GoToShootPos(destination);
    }

    private IEnumerator UpdateLineRendererPositions()
    {
        while (true)
        {
            for (int i = 0; i < Anchors.Count; i++)
            {
                LineRenderer.SetPosition(i, Anchors[i].position);
            }

            LineRenderer.SetPosition(LineRenderer.positionCount - 1, transform.position);
            yield return null;
        }
    }

    //pulls the grappling hook back to the player, once it reached the player set it to inactive
    private void PullBack()
    {
        //only pullback when we aren't already pulling back and the we are not in the inactive state. 
        if (CurrentHookState == HookStates.BusyPullingBack || CurrentHookState == HookStates.Inactive) return;

        CurrentHookState = HookStates.BusyPullingBack;

        List<Vector2> returnPoints = new List<Vector2>();
        foreach (Transform t in Anchors)
        {
            returnPoints.Add(t.position);
        }

        returnPoints.Add(transform.position);

        HookProjectileScript.Returned = DeactivateHook;
        HookProjectileScript.Return(returnPoints);
    }

    private void DestroyAnchors()
    {
        foreach (Transform t in Anchors)
        {
            Destroy(t.gameObject);
        }
        Anchors.Clear();

    }

    private void StopLineRenderer() {
        LineRenderer.enabled = false;
        StopCoroutine(_lineUpdateCoroutine);

        print("stopped coroutine");

        LineRenderer.positionCount = 0;
    }
}