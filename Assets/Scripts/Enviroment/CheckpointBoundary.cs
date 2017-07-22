using UnityEngine;

public class CheckpointBoundary : MonoBehaviour {

    [SerializeField] private LayerMask layers;
    [SerializeField] private float maxDistance = 30;
    [SerializeField] private float xScale = 0.5f;

    [SerializeField] private GameObject topBoundary;
    [SerializeField] private GameObject bottomBoundary;

    [ContextMenu("Generate Boundary")]
    private void GenerateBoundary() {
        if (topBoundary != null) {
            UpdateTopBoundary();
        }
        if (bottomBoundary != null) {
            UpdateBottomBoundary();
        }
    }

    private void UpdateTopBoundary() {
        RaycastHit2D topRaycastHit = Physics2D.Raycast(transform.position, transform.up, maxDistance, layers);
        float localTopBoundaryLength = topRaycastHit.distance;
        float worldTopBoundaryLength = localTopBoundaryLength / transform.localScale.y;

        topBoundary.transform.localScale = new Vector2(xScale, worldTopBoundaryLength);
        topBoundary.transform.localPosition = new Vector2(0, topBoundary.transform.localScale.y / 2);
    }

    private void UpdateBottomBoundary() {
        RaycastHit2D bottomRaycastHit = Physics2D.Raycast(transform.position, -transform.up, maxDistance, layers);
        float localBottomBoundaryLength = bottomRaycastHit.distance;
        float worldBottomBoundaryLength = localBottomBoundaryLength / transform.localScale.y;

        bottomBoundary.transform.localScale = new Vector2(xScale, worldBottomBoundaryLength);
        bottomBoundary.transform.localPosition = new Vector2(0, -bottomBoundary.transform.localScale.y / 2);
    }

}
