﻿using IoCPlus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookView : View, IHook, ITriggerer {

    public HookState ActiveHookState { get { return activeHookState; } }
    public HookState LastHookState { get { return lastHookState; } }
    public List<Transform> Anchors { get { return anchors; } }
    public LineRenderer LineRenderer { get { return lineRenderer; } }
    public List<int> HookableLayers { get { return hookableLayers; } }
    public LayerMask RayLayers { get { return rayLayers; } }
    public float DirectionSpeedNeutralValue { get { return directionSpeedNeutralValue; } }
    public float MinimalDistanceFromOwner { get { return minimalDistanceFromOwner; } }

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<IHook> hookRef;

    [Inject] private UpdateHookEvent updateHookEvent;

    [SerializeField] private LayerMask rayLayers;
    [SerializeField] private GameObject hookLinePrefab;
    [SerializeField] private float directionSpeedNeutralValue = 0.15f;
    [SerializeField] private float minimalDistanceFromOwner = 0.75f;

    private LineRenderer lineRenderer;
    private List<int> hookableLayers;

    private HookState activeHookState = HookState.Inactive;
    private HookState lastHookState = HookState.Inactive;
    private List<Transform> anchors = new List<Transform>();

    private Coroutine hookUpdateCoroutine;

    public override void Initialize() {
        hookRef.Set(this);
    }

    public void SetHookState(HookState hookstate) {
        lastHookState = activeHookState;
        activeHookState = hookstate;
    }

    public void AddAnchor(Vector2 position, Transform parent) {
        anchors.Insert(0, CreateAnchor(position, parent));
        lineRenderer.positionCount = anchors.Count + 1;
        SetLineRendererPositions();
    }

    public void DestroyAnchorAt(int index) {
        UnityEngine.Object.Destroy(anchors[index].gameObject);
        anchors.RemoveAt(index);
        lineRenderer.positionCount = anchors.Count + 1;
        SetLineRendererPositions();
    }

    public void DestroyAnchors() {
        foreach (Transform t in anchors) {
            UnityEngine.Object.Destroy(t.gameObject);
        }
        anchors.Clear();
    }

    public void ActivateHook() {
        if (hookUpdateCoroutine != null) { return; }
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, anchors[0].position);
        hookUpdateCoroutine = StartCoroutine(UpdateHook());
    }

    public void DeactivateHook() {
        StopCoroutine(hookUpdateCoroutine);
        hookUpdateCoroutine = null;
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
    }

    private Transform CreateAnchor(Vector2 position, Transform parent) {
        GameObject anchor = new GameObject("HookRopeAnchor");
        anchor.transform.SetParent(parent);
        anchor.transform.position = position;
        anchor.layer = LayerMask.NameToLayer("Ignore Raycast");
        return anchor.transform;
    }

    private IEnumerator UpdateHook() {
        while (true) {
            updateHookEvent.Dispatch();
            SetLineRendererPositions();
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetLineRendererPositions() {
        lineRenderer.SetPosition(0, transform.position);
        for (int i = 0; i < anchors.Count; i++) {
            lineRenderer.SetPosition(i + 1, anchors[i].position);
        }
    }

    private void Awake() {
        lineRenderer = Instantiate(hookLinePrefab, transform).GetComponent<LineRenderer>();
        hookableLayers = HookableLayer.GetHookableLayers();
    }
}