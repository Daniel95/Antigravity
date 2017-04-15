using IoCPlus;
using System.Collections;
using UnityEngine;

public class AimLineView : View {

    public Vector2 LineDestination { set { lineDestination = value; } }
    public bool AimLineActive { get { return aimLineActive; } }

    [SerializeField] private LineRenderer line;

    private Vector2 lineDestination;
    private Coroutine updateLineRendererPositions;
    private bool aimLineActive;

    void Start() {
        line.enabled = false;
    }

    public void StartAimLine(Vector2 destination) {
        line.enabled = aimLineActive = true;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, destination);

        updateLineRendererPositions = StartCoroutine(UpdateLineRendererPositions());
    }

    IEnumerator UpdateLineRendererPositions() {
        while (true)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, lineDestination);
            yield return null;
        }
    }

    public void StopAimLine() {
        if (aimLineActive) {
            StopCoroutine(updateLineRendererPositions);

            line.enabled = aimLineActive = false;
        }
    }
}
