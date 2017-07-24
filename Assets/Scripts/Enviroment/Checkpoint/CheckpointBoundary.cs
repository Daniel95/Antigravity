using UnityEngine;

public class CheckpointBoundary : MonoBehaviour {
    
    private GameObject Checkpoint { get { return checkpoint; } }

    [SerializeField] private float xScale = 0.5f;

    private GameObject checkpoint;

    public void UpdateBoundary(Transform checkPointTransform, RaycastHit2D raycastHitUp, RaycastHit2D raycastHitDown) {
        if(checkPointTransform.parent != null) {
            transform.SetParent(checkPointTransform.parent);
        }
        checkpoint = checkPointTransform.gameObject;

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
