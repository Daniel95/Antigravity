using IoCPlus;
using System.Collections;
using UnityEngine;

public class CharacterAimLineView : View, ICharacterAimLine {

    public bool AimLineActive { get { return aimLineActive; } }

    [SerializeField] private GameObject aimLinePrefab;

    [Inject] private Ref<ICharacterAimLine> characterAimLineRef;

    private LineRenderer lineRenderer;
    private Vector2 lineDestination;
    private Coroutine updateLineRendererPositionsCoroutine;
    private bool aimLineActive;

    public override void Initialize() {
        characterAimLineRef.Set(this);
    }

    public void UpdateAimLineDestination(Vector2 destination) {
        if (!aimLineActive) {
            StartAimLine(destination);
        }

        lineDestination = destination;
    }

    public void StopAimLine() {
        if (!aimLineActive) { return; }
        StopCoroutine(updateLineRendererPositionsCoroutine);
        updateLineRendererPositionsCoroutine = null;
        lineRenderer.enabled = aimLineActive = false;
    }

    private void Awake() {
        lineRenderer = Instantiate(aimLinePrefab, transform).GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void StartAimLine(Vector2 destination) {
        lineRenderer.enabled = aimLineActive = true;

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
}
