using IoCPlus;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HookView : View, IHookView, IWeaponOutput, ITriggerer {

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<HookModel> hookModel;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject hookProjectileGObj;
    [SerializeField] private HookProjectile hookProjectileScript;
    [SerializeField] private LayerMask rayLayers;
    [SerializeField] private GameObject hookProjectilePrefab;
    [SerializeField] private float directionSpeedNeutralValue = 0.15f;
    [SerializeField] private AimRayView aimRay;

    //to save our LineUpdateCoroutine;
    private Coroutine _lineUpdateCoroutine;
    private Coroutine _holdGrappleCoroutine;

    public override void Initialize() {
        hookModel.Get().LineRendererComponent = lineRenderer;

        hookProjectileGObj = Instantiate(hookProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
        hookProjectileScript = hookProjectileGObj.GetComponent<HookProjectile>();
        hookProjectileGObj.SetActive(false);
    }

    public void Aiming(Vector2 destination, Vector2 spawnPosition) {
        if (!aimRay.AimRayActive) {
            aimRay.StartAimRay(destination);
        }

        aimRay.RayDestination = destination;
    }

    /// <summary>
    /// cancel aiming
    /// </summary>
    public void CancelAiming() {
        StopBulletTime();
    }

    private void StopBulletTime() {
        aimRay.StopAimRay();
    }

    /// <summary>
    /// spawns the grapple projectile and activates its moveTowards script
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="spawnPosition"></param>
    public void Fire(Vector2 destination, Vector2 spawnPosition) {
        StopBulletTime();

        if (hookModel.Get().CurrentHookState == HookModel.HookStates.Inactive) {
            print("shoot : " + this);
            ShootHook(destination, spawnPosition);
        }
        //if we still have a grapple activate, deactivate it first before we shoot a new one
        else if (hookModel.Get().CurrentHookState == HookModel.HookStates.Active || hookModel.Get().CurrentHookState == HookModel.HookStates.BusyShooting) {
            _holdGrappleCoroutine = StartCoroutine(HoldGrapple(destination, spawnPosition));
            PullBack();
        }
    }

    protected virtual void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        aimRay = GetComponent<AimRayView>();

        hookProjectileGObj = Instantiate(hookProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
        hookProjectileScript = hookProjectileGObj.GetComponent<HookProjectile>();
        hookProjectileGObj.SetActive(false);
    }

    protected virtual void Hooked(int hookedLayer) { }

    protected virtual void Canceled() {
        hookProjectileScript.Attached = null;

        if (_holdGrappleCoroutine != null)
            StopCoroutine(_holdGrappleCoroutine);

        PullBack();
    }

    protected void ChangeSpeedByAngle() {
        //change speed by calculating the angle
        //float angleDifference =
         //   Mathf.Abs(Vector2.Dot((hookProjectileGObj.transform.position - transform.position).normalized,
         //       _charAcces.ControlVelocity.GetVelocityDirection()));

       // float speedChange = angleDifference * -1 + 1;

       // _charAcces.ControlSpeed.TempSpeedChange(speedChange, directionSpeedNeutralValue);
    }

    public void AddAnchor(Vector2 position, Transform parent) {
        hookModel.Get().Anchors.Add(CreateAnchor(position, parent));
    }

    private Transform CreateAnchor(Vector2 pos, Transform parent) {
        GameObject p = new GameObject("GrappleAnchor");
        p.transform.position = pos;
        p.layer = LayerMask.NameToLayer("Ignore Raycast");
        p.transform.SetParent(parent);
        return p.transform;
    }

    protected virtual void DeactivateHook() {
        hookProjectileScript.Returned = null;
        hookModel.Get().CurrentHookState = HookModel.HookStates.Inactive;

        hookProjectileGObj.SetActive(false);

        print("DestroyAnchors");
        DestroyAnchors();
        print("StopLinerenderer");
        StopLineRenderer();
    }

    private IEnumerator HoldGrapple(Vector2 destination, Vector2 spawnPosition) {
        while (hookModel.Get().CurrentHookState != HookModel.HookStates.Inactive) {
            yield return null;
        }

        print("shoot");


        ShootHook(destination, spawnPosition);
    }

    private void ShootHook(Vector2 destination, Vector2 spawnPosition) {
        hookModel.Get().CurrentHookState = HookModel.HookStates.BusyShooting;

        hookProjectileGObj.SetActive(true);
        hookProjectileGObj.transform.position = spawnPosition;

        Vector2 anchorPos = hookProjectileGObj.transform.position + (hookProjectileGObj.transform.position - transform.position).normalized * 0.1f;
        hookModel.Get().Anchors.Add(CreateAnchor(anchorPos, hookProjectileGObj.transform));

        //activate line renderer
        lineRenderer.enabled = true;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, hookModel.Get().Anchors[0].position);
        lineRenderer.SetPosition(1, transform.position);

        print("Start coroutine");
        _lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());

        hookProjectileScript.Attached = Hooked;
        hookProjectileScript.Canceled = Canceled;
        hookProjectileScript.GoToShootPos(destination);
    }

    private IEnumerator UpdateLineRendererPositions() {
        while (true) {
            for (int i = 0; i < hookModel.Get().Anchors.Count; i++) {
                lineRenderer.SetPosition(i, hookModel.Get().Anchors[i].position);
            }

            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
            yield return null;
        }
    }

    //pulls the grappling hook back to the player, once it reached the player set it to inactive
    private void PullBack() {
        //only pullback when we aren't already pulling back and the we are not in the inactive state. 
        if (hookModel.Get().CurrentHookState == HookModel.HookStates.BusyPullingBack || hookModel.Get().CurrentHookState == HookModel.HookStates.Inactive) return;

        hookModel.Get().CurrentHookState = HookModel.HookStates.BusyPullingBack;

        List<Vector2> returnPoints = new List<Vector2>();
        foreach (Transform t in hookModel.Get().Anchors) {
            returnPoints.Add(t.position);
        }

        returnPoints.Add(transform.position);

        hookProjectileScript.Returned = DeactivateHook;
        hookProjectileScript.Return(returnPoints);
    }

    private void DestroyAnchors() {
        foreach (Transform t in hookModel.Get().Anchors) {
            Destroy(t.gameObject);
        }
        hookModel.Get().Anchors.Clear();

    }

    private void StopLineRenderer() {
        lineRenderer.enabled = false;
        StopCoroutine(_lineUpdateCoroutine);

        print("stopped coroutine");

        lineRenderer.positionCount = 0;
    }
}