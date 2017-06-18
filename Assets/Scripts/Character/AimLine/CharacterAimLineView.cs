using IoCPlus;
using System.Collections;
using UnityEngine;

public class CharacterAimLineView : View, ICharacterAimLine {

    public bool AimLineActive { get { return aimLineIsActive; } }

    [SerializeField] private GameObject aimLinePrefab;

    private LineRenderer lineRenderer;
    private Vector2 lineDestination;
    private Coroutine updateLineRendererPositionsCoroutine;
    private bool aimLineIsActive;

    public void UpdateAimLineDestination(Vector2 destination) {
        if (!aimLineIsActive) {
            StartAimLine(destination);
        }

        lineDestination = destination;
    }

    public void StopAimLine() {
        if (!aimLineIsActive) { return; }
        StopCoroutine(updateLineRendererPositionsCoroutine);
        updateLineRendererPositionsCoroutine = null;
        lineRenderer.enabled = false;
        aimLineIsActive = false;
    }

    private void StartAimLine(Vector2 destination) {
        lineRenderer.enabled = aimLineIsActive = true;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, destination);

        updateLineRendererPositionsCoroutine = StartCoroutine(UpdateLineRendererPositions());
    }

    IEnumerator UpdateLineRendererPositions() {
        while (true)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, lineDestination);
            yield return null;
        }
    }

    private void Awake() {
        lineRenderer = Instantiate(aimLinePrefab, transform).GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
}
