using UnityEngine;

public class CheckpointBoundary : MonoBehaviour {
    
    [SerializeField] private float xScale = 0.5f;

    public void UpdateBoundary(Transform checkPointTransform, RaycastHit2D raycastHitUp, RaycastHit2D raycastHitDown) {
        float boundaryTopLength = raycastHitUp.distance;
        float boundaryBottomLength = raycastHitDown.distance;

        float boundaryLocalLength = boundaryTopLength + boundaryBottomLength;
        float boundaryWorldLength = boundaryLocalLength / checkPointTransform.localScale.y;

        transform.localScale = new Vector2(xScale, boundaryWorldLength + xScale * 2);

        Vector2 topPosition = raycastHitUp.point;
        Vector2 bottomPosition = raycastHitDown.point;
        Vector2 boundaryPosition = (bottomPosition + topPosition) / 2;

        transform.position = boundaryPosition;
    }

}
