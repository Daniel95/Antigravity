using UnityEngine;
using System.Collections;

public class CheckpointView : MonoBehaviour {

    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private float maxRaycastDistance = 30;

    private const string CHECKPOINT_BOUNDARY_PREFAB_PATH = "Enviroment/CheckpointBoundary";

    [SerializeField] private string checkPointBoundaryId;

    private Coroutine drawDebugBoundaryCoroutine;

    [ContextMenu("GenerateBoundary")]
    private void GenerateBoundary() {
        CheckpointBoundary checkpointBoundary = GetCheckpointBoundary();
        checkpointBoundary.UpdateBoundary(transform, GetRaycastHitUp(), GetRaycastHitDown());
        Debug.Log(name + " generates a boundary at " + checkpointBoundary.transform.position);
    }


    private CheckpointBoundary GetCheckpointBoundary() {
        if(!string.IsNullOrEmpty(checkPointBoundaryId)) {
            CheckpointBoundary foundCheckpointBounadry = FindObjectIdCheckpointBoundary();

            if(foundCheckpointBounadry != null) {
                return foundCheckpointBounadry;
            } 
        }

        CheckpointBoundary generatedCheckpointBoundary = GenerateCheckpointBoundary();
        return generatedCheckpointBoundary;
    }

    private CheckpointBoundary FindObjectIdCheckpointBoundary() {
        ObjectId[] objectIds = FindObjectsOfType<ObjectId>();

        foreach (ObjectId objectId in objectIds) {
            if (objectId.Id == checkPointBoundaryId) {
                GameObject objectIdGameObject = objectId.gameObject;
                CheckpointBoundary foundCheckPointBoundary = objectIdGameObject.GetComponent<CheckpointBoundary>();
                return foundCheckPointBoundary;
            }
        }

        return null;
    }

    private CheckpointBoundary GenerateCheckpointBoundary() {
        CheckpointBoundary checkPointBoundaryPrefab = Resources.Load<CheckpointBoundary>(CHECKPOINT_BOUNDARY_PREFAB_PATH);
        CheckpointBoundary checkPointBoundary = Instantiate(checkPointBoundaryPrefab);
        ObjectId checkpointBoundaryObjectId = checkPointBoundary.gameObject.GetComponent<ObjectId>();
        checkpointBoundaryObjectId.GenerateId();
        checkPointBoundaryId = checkpointBoundaryObjectId.Id;
        return checkPointBoundary;
    }

    private RaycastHit2D GetRaycastHitUp() {
        RaycastHit2D raycastHitUp = Physics2D.Raycast(transform.position, transform.up, maxRaycastDistance, raycastLayers);
        return raycastHitUp;
    }

    private RaycastHit2D GetRaycastHitDown() {
        RaycastHit2D raycastHitDown = Physics2D.Raycast(transform.position, -transform.up, maxRaycastDistance, raycastLayers);
        return raycastHitDown;
    }

    private void Awake() {
        GenerateBoundary();
    }

    private void OnValidate() {
        UnityEditor.EditorApplication.delayCall += () => {
            GenerateBoundary();
            StartDrawDebugBoundaries();
        };
    }

    private void Reset() {
        GenerateBoundary();
        StartDrawDebugBoundaries();
    }

    private void OnDestroy() {
        CheckpointBoundary checkpointBoundary = FindObjectIdCheckpointBoundary();
        if(checkpointBoundary == null) { return; }
        if (Application.isPlaying) {
            Destroy(checkpointBoundary.gameObject);
        } else {
            DestroyImmediate(checkpointBoundary.gameObject);
        }
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

    private void DestroyAllBoundaries() {
        CheckpointBoundary[] checkpointBoundaries = FindObjectsOfType<CheckpointBoundary>();

        if (Application.isPlaying) {
            foreach (CheckpointBoundary boundary in checkpointBoundaries) {
                Destroy(boundary.gameObject);
            }
        } else {
            foreach (CheckpointBoundary boundary in checkpointBoundaries) {
                DestroyImmediate(boundary.gameObject);
            }
        }
    }

}