using UnityEngine;
using System.Collections;

public class CheckpointView : MonoBehaviour {

    [SerializeField] private LayerMask layers;
    [SerializeField] private float maxDistance = 30;

    private const string CHECKPOINT_BOUNDARY_PATH = "Enviroment/CheckpointBoundary";

    private Coroutine drawDebugBoundaryCoroutine;

    private RaycastHit2D GetRaycastHitUp() {
        RaycastHit2D raycastHitUp = Physics2D.Raycast(transform.position, transform.up, maxDistance, layers);
        return raycastHitUp;
    }

    private RaycastHit2D GetRaycastHitDown() {
        RaycastHit2D raycastHitDown = Physics2D.Raycast(transform.position, -transform.up, maxDistance, layers);
        return raycastHitDown;
    }

    private void Awake() {
        CheckpointBoundary checkPointBoundary = Resources.Load<CheckpointBoundary>(CHECKPOINT_BOUNDARY_PATH);

        Instantiate(checkPointBoundary.gameObject);
        checkPointBoundary.UpdateBoundary(transform, GetRaycastHitUp(), GetRaycastHitDown());
    }

    private void OnValidate() {
        StartDrawDebugBoundaries();
    }

    private void Reset() {
        StartDrawDebugBoundaries();
    }

    private void StartDrawDebugBoundaries() {
#if UNITY_EDITOR
        if(!Application.isPlaying && gameObject.activeInHierarchy && drawDebugBoundaryCoroutine == null) {
            drawDebugBoundaryCoroutine = StartCoroutine(DrawDebugBoundary());
        }
#endif
    }

    private IEnumerator DrawDebugBoundary() {
        while (true) {
            Vector2 topPosition = GetRaycastHitUp().point;
            Vector2 bottomPosition = GetRaycastHitDown().point;
            Debug.DrawLine(topPosition, bottomPosition, Color.yellow);
            yield return null;
        }
    }

}
