using UnityEngine;
using System.Collections;
using IoCPlus;

public class CheckpointView : View, ICheckpoint {

    public GameObject CheckpointGameObject { get { return gameObject; } }
    public GameObject CheckpointBoundaryGameObject { get { return checkpointBoundary.gameObject; } }

    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private float maxRaycastDistance = 30;
    [SerializeField] [ObjectIdRef] private string checkPointBoundaryId;

    [Inject] private Refs<ICheckpoint> checkpointRefs;

    private const string CHECKPOINT_BOUNDARY_PREFAB_PATH = "Enviroment/CheckpointBoundary";

    private CheckpointBoundary checkpointBoundary;
    private Coroutine drawDebugBoundaryCoroutine;

    public override void Initialize() {
        checkpointRefs.Add(this);
    }

    public override void Dispose() {
        checkpointRefs.Remove(this);
    }

    protected override void OnDestroy() {
        CheckpointBoundary checkpointBoundary = FindObjectIdCheckpointBoundary();
        if(checkpointBoundary == null) { return; }
        if (Application.isPlaying) {
            Destroy(checkpointBoundary.gameObject);
        } else {
            DestroyImmediate(checkpointBoundary.gameObject);
        }
    }

    [ContextMenu("Spawn Checkpoint Boundary")]
    private void GenerateBoundary() {
        checkpointBoundary = GetCheckpointBoundary();
        checkpointBoundary.UpdateBoundary(transform, GetRaycastHitUp(), GetRaycastHitDown());
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
        GameObject foundCheckPointBoundaryGameObject = ObjectId.Find(checkPointBoundaryId);
        if(foundCheckPointBoundaryGameObject == null) { return null; }

        CheckpointBoundary foundCheckpointBoundary = foundCheckPointBoundaryGameObject.GetComponent<CheckpointBoundary>();
        return foundCheckpointBoundary;
    }

    private CheckpointBoundary GenerateCheckpointBoundary() {
        CheckpointBoundary checkPointBoundaryPrefab = Resources.Load<CheckpointBoundary>(CHECKPOINT_BOUNDARY_PREFAB_PATH);
        CheckpointBoundary checkPointBoundary = Instantiate(checkPointBoundaryPrefab);
        ObjectId checkpointBoundaryObjectId = checkPointBoundary.gameObject.GetComponent<ObjectId>();
        checkpointBoundaryObjectId.GenerateId();
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

    private void Start() {
        if(ObjectId.Find(checkPointBoundaryId) == null) {
            Debug.LogWarning("No reference to checkpoint boundary", this);
        }
    }

    private void OnValidate() {
        StartDrawDebugBoundaries();
    }

    private void Reset() {
        GenerateBoundary();
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
