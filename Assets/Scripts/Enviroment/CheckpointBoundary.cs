using UnityEngine;

public class CheckpointBoundary : MonoBehaviour {
    
    [SerializeField] private float xScale = 0.5f;

    public void UpdateBoundary(Transform checkPointTransform, RaycastHit2D raycastHitUp, RaycastHit2D raycastHitDown) {
        float boundaryTopLength = raycastHitUp.distance;
        float boundaryBottomLength = raycastHitDown.distance;

        float boundaryLength = boundaryTopLength + boundaryBottomLength;

        transform.localScale = new Vector2(xScale, boundaryLength);

        Vector2 topPosition = raycastHitUp.point;
        Vector2 bottomPosition = raycastHitDown.point;
        Vector2 boundaryPosition = (bottomPosition + topPosition) / 2;

        transform.position = boundaryPosition;
        transform.rotation = checkPointTransform.rotation;
    }

}
