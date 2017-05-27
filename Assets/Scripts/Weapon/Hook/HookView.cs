using IoCPlus;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HookView : View, IHook, ITriggerer {

    public HookState ActiveHookState { get { return activeHookState; } set { activeHookState = value; } }
    public HookState LastHookState { get { return lastHookState; } set { lastHookState = value; } }
    public List<Transform> Anchors { get { return anchors; } }
    public LineRenderer LineRenderer { get { return lineRenderer; } }
    public LayerMask RayLayers { get { return rayLayers; } }
    public float DirectionSpeedNeutralValue { get { return directionSpeedNeutralValue; } }
    public float MinimalHookDistance { get { return minimalHookDistance; } }
    public GameObject Owner { get { return gameObject; } }
    public Vector2 Destination { get { return destination; } set { destination = value; } }

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<IHook> hookRef;

    [SerializeField] private LayerMask rayLayers;
    [SerializeField] private GameObject hookLinePrefab;
    [SerializeField] private GameObject hookProjectilePrefab;
    [SerializeField] private float directionSpeedNeutralValue = 0.15f;
    [SerializeField] private float minimalHookDistance = 0.75f;

    private LineRenderer lineRenderer;

    private HookState activeHookState = HookState.Inactive;
    private HookState lastHookState = HookState.Inactive;
    private List<Transform> anchors = new List<Transform>();
    private Vector2 destination;

    private Coroutine lineUpdateCoroutine;

    public override void Initialize() {
        hookRef.Set(this);
    }

    public void AddAnchor(Vector2 position, Transform parent) {
        anchors.Add(CreateAnchor(position, parent));
        lineRenderer.positionCount = anchors.Count + 1;
        SetLineRenderenPositions();
    }

    public void DestroyAnchors() {
        foreach (Transform t in anchors) {
            UnityEngine.Object.Destroy(t.gameObject);
        }
        anchors.Clear();
    }

    public void ActivateHookRope() {
        if (lineUpdateCoroutine != null) { return; }
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, anchors[0].position);
        lineRenderer.SetPosition(1, transform.position);
        lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());
    }

    public void DeactivateHookRope() {
        StopCoroutine(lineUpdateCoroutine);
        lineUpdateCoroutine = null;
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

    private IEnumerator UpdateLineRendererPositions() {
        while (true) {
            SetLineRenderenPositions();
            yield return null;
        }
    }

    private void SetLineRenderenPositions() {
        for (int i = 0; i < anchors.Count; i++) {
            lineRenderer.SetPosition(i, anchors[i].position);
        }
        lineRenderer.SetPosition(anchors.Count, transform.position);
    }

    private void Awake() {
        lineRenderer = Instantiate(hookLinePrefab, transform).GetComponent<LineRenderer>();
    }
}