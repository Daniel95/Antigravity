﻿using IoCPlus;
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
    public List<Transform> Anchors { get { return anchors; } }
    public LineRenderer LineRendererComponent { get { return lineRendererComponent; } }
    public LayerMask RayLayers { get { return rayLayers; } }
    public float DirectionSpeedNeutralValue { get { return directionSpeedNeutralValue; } }

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject hookProjectileGObj;
    [SerializeField] private HookProjectileView hookProjectileScript;
    [SerializeField] private LayerMask rayLayers;
    [SerializeField] private GameObject hookProjectilePrefab;
    [SerializeField] private float directionSpeedNeutralValue = 0.15f;

    private HookState currentHookState = HookState.Inactive;
    private GameObject hookProjectileGameObject;
    private HookProjectileView hookProjectile;
    private List<Transform> anchors = new List<Transform>();
    private LineRenderer lineRendererComponent;

    //to save our LineUpdateCoroutine;
    private Coroutine lineUpdateCoroutine;
    private Coroutine holdGrappleCoroutine;

    public override void Initialize() {
        lineRendererComponent = lineRenderer;

        hookProjectileGObj = Instantiate(hookProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
        hookProjectileScript = hookProjectileGObj.GetComponent<HookProjectileView>();
        hookProjectileGObj.SetActive(false);
    }

    /// <summary>
    /// spawns the grapple projectile and activates its moveTowards script
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="spawnPosition"></param>
    public void Fire(Vector2 destination, Vector2 spawnPosition) {
        if (currentHookState == HookState.Inactive) {
            print("shoot : " + this);
            ShootHook(destination, spawnPosition);
        }
        //if we still have a grapple activate, deactivate it first before we shoot a new one
        else if (currentHookState == HookState.Active || currentHookState == HookState.BusyShooting) {
            holdGrappleCoroutine = StartCoroutine(HoldGrapple(destination, spawnPosition));
            PullBack();
        }
    }

    protected virtual void Awake() {
        lineRenderer = GetComponent<LineRenderer>();

        hookProjectileGObj = Instantiate(hookProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
        hookProjectileScript = hookProjectileGObj.GetComponent<HookProjectileView>();
        hookProjectileGObj.SetActive(false);
    }

    protected virtual void Hooked(int hookedLayer) { }

    protected virtual void Canceled() {
        hookProjectileScript.Attached = null;

        if (holdGrappleCoroutine != null)
            StopCoroutine(holdGrappleCoroutine);

        PullBack();
    }

    public void SpawnAnchor(Vector2 position, Transform parent) {
        anchors.Add(CreateAnchor(position, parent));
    }

    public void ActivateHookProjectile(Vector2 spawnPosition) {
        hookProjectileGObj.SetActive(true);
        hookProjectileGObj.transform.position = spawnPosition;
    }

    public void DeactivateHookProjectile() {
        hookProjectileGObj.SetActive(false);
    }

    private Transform CreateAnchor(Vector2 position, Transform parent) {
        GameObject anchor = new GameObject("HookRopeAnchor");
        anchor.transform.position = position;
        anchor.layer = LayerMask.NameToLayer("Ignore Raycast");
        anchor.transform.SetParent(parent);
        return anchor.transform;
    }

    protected virtual void DeactivateHook() {
        hookProjectileScript.Returned = null;
        currentHookState = HookState.Inactive;

        hookProjectileGObj.SetActive(false);

        DestroyAnchors();
        DeactivateHookRope();
    }

    private IEnumerator HoldGrapple(Vector2 destination, Vector2 spawnPosition) {
        while (currentHookState != HookState.Inactive) {
            yield return null;
        }

        ShootHook(destination, spawnPosition);
    }

    private void ShootHook(Vector2 destination, Vector2 spawnPosition) {
        currentHookState = HookState.BusyShooting;

        hookProjectileGObj.SetActive(true);
        hookProjectileGObj.transform.position = spawnPosition;

        Vector2 anchorPos = hookProjectileGObj.transform.position + (hookProjectileGObj.transform.position - transform.position).normalized * 0.1f;
        anchors.Add(CreateAnchor(anchorPos, hookProjectileGObj.transform));

        //activate line renderer
        lineRenderer.enabled = true;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, anchors[0].position);
        lineRenderer.SetPosition(1, transform.position);

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
        if (currentHookState == HookState.BusyPullingBack || currentHookState == HookState.Inactive) return;

        currentHookState = HookState.BusyPullingBack;

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

    public void ActivateHookRope() {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, anchors[0].position);
        lineRenderer.SetPosition(1, transform.position);
        lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());
    }

    public void DeactivateHookRope() {
        lineRenderer.enabled = false;
        StopCoroutine(lineUpdateCoroutine);
        lineRenderer.positionCount = 0;
    }
}