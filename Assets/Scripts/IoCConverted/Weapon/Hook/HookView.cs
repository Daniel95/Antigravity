using IoCPlus;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HookView : View, IHook, ITriggerer {

    public static class HookAbleLayers {
        public static int GrappleSurface = LayerMask.NameToLayer("GrappleHook");
        public static int PullSurface = LayerMask.NameToLayer("PullHook");
    }

    public HookState CurrentHookState { get { return currentHookState; } }
    public GameObject HookProjectileGameObject { get { return hookProjectileGameObject; } }
    public HookProjectile HookProjectile { get { return hookProjectile; } }
    public List<Transform> Anchors { get { return anchors; } }
    public LineRenderer LineRendererComponent { get { return lineRendererComponent; } }

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject hookProjectileGObj;
    [SerializeField] private HookProjectile hookProjectileScript;
    [SerializeField] private LayerMask rayLayers;
    [SerializeField] private GameObject hookProjectilePrefab;
    [SerializeField] private float directionSpeedNeutralValue = 0.15f;
    [SerializeField] private CharacterAimLineView aimRay;

    private HookState currentHookState = HookState.Inactive;
    private GameObject hookProjectileGameObject;
    private HookProjectile hookProjectile;
    private List<Transform> anchors = new List<Transform>();
    private LineRenderer lineRendererComponent;

    //to save our LineUpdateCoroutine;
    private Coroutine lineUpdateCoroutine;
    private Coroutine holdGrappleCoroutine;

    public override void Initialize() {
        lineRendererComponent = lineRenderer;

        hookProjectileGObj = Instantiate(hookProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
        hookProjectileScript = hookProjectileGObj.GetComponent<HookProjectile>();
        hookProjectileGObj.SetActive(false);
    }

    /// <summary>
    /// spawns the grapple projectile and activates its moveTowards script
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="spawnPosition"></param>
    public void Fire(Vector2 destination, Vector2 spawnPosition) {
        if (currentHookState == HookStates.Inactive) {
            print("shoot : " + this);
            ShootHook(destination, spawnPosition);
        }
        //if we still have a grapple activate, deactivate it first before we shoot a new one
        else if (currentHookState == HookStates.Active || currentHookState == HookStates.BusyShooting) {
            holdGrappleCoroutine = StartCoroutine(HoldGrapple(destination, spawnPosition));
            PullBack();
        }
    }

    protected virtual void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        aimRay = GetComponent<CharacterAimLineView>();

        hookProjectileGObj = Instantiate(hookProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
        hookProjectileScript = hookProjectileGObj.GetComponent<HookProjectile>();
        hookProjectileGObj.SetActive(false);
    }

    protected virtual void Hooked(int hookedLayer) { }

    protected virtual void Canceled() {
        hookProjectileScript.Attached = null;

        if (holdGrappleCoroutine != null)
            StopCoroutine(holdGrappleCoroutine);

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
        anchors.Add(CreateAnchor(position, parent));
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
        currentHookState = HookStates.Inactive;

        hookProjectileGObj.SetActive(false);

        print("DestroyAnchors");
        DestroyAnchors();
        print("StopLinerenderer");
        StopLineRenderer();
    }

    private IEnumerator HoldGrapple(Vector2 destination, Vector2 spawnPosition) {
        while (currentHookState != HookStates.Inactive) {
            yield return null;
        }

        print("shoot");


        ShootHook(destination, spawnPosition);
    }

    private void ShootHook(Vector2 destination, Vector2 spawnPosition) {
        currentHookState = HookStates.BusyShooting;

        hookProjectileGObj.SetActive(true);
        hookProjectileGObj.transform.position = spawnPosition;

        Vector2 anchorPos = hookProjectileGObj.transform.position + (hookProjectileGObj.transform.position - transform.position).normalized * 0.1f;
        anchors.Add(CreateAnchor(anchorPos, hookProjectileGObj.transform));

        //activate line renderer
        lineRenderer.enabled = true;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, anchors[0].position);
        lineRenderer.SetPosition(1, transform.position);

        print("Start coroutine");
        lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());

        hookProjectileScript.Attached = Hooked;
        hookProjectileScript.Canceled = Canceled;
        hookProjectileScript.GoToShootPos(destination);
    }

    private IEnumerator UpdateLineRendererPositions() {
        while (true) {
            for (int i = 0; i < anchors.Count; i++) {
                lineRenderer.SetPosition(i, anchors[i].position);
            }

            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
            yield return null;
        }
    }

    //pulls the grappling hook back to the player, once it reached the player set it to inactive
    private void PullBack() {
        //only pullback when we aren't already pulling back and the we are not in the inactive state. 
        if (currentHookState == HookStates.BusyPullingBack || currentHookState == HookStates.Inactive) return;

        currentHookState = HookStates.BusyPullingBack;

        List<Vector2> returnPoints = new List<Vector2>();
        foreach (Transform t in anchors) {
            returnPoints.Add(t.position);
        }

        returnPoints.Add(transform.position);

        hookProjectileScript.Returned = DeactivateHook;
        hookProjectileScript.Return(returnPoints);
    }

    private void DestroyAnchors() {
        foreach (Transform t in anchors) {
            Destroy(t.gameObject);
        }
        anchors.Clear();

    }

    private void StopLineRenderer() {
        lineRenderer.enabled = false;
        StopCoroutine(lineUpdateCoroutine);

        print("stopped coroutine");

        lineRenderer.positionCount = 0;
    }
}