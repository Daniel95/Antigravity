using UnityEngine;
using System.Collections;
using IoCPlus;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(AnimatedBody))]
public class CheckpointView : View, ICheckpoint {

    public bool Unlocked { get { return unlocked; } set { unlocked = value; } }
    public GameObject CheckpointGameObject { get { return gameObject; } }
    public GameObject CheckpointBoundaryGameObject { get { return FindObjectIdCheckpointBoundary().gameObject; } }

    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private float maxRaycastDistance = 30;
    [SerializeField] [ObjectIdRef] private string checkPointBoundaryId;

    [Inject] private Refs<ICheckpoint> checkpointRefs;

    private const string CHECKPOINT_BOUNDARY_PREFAB_PATH = "Enviroment/CheckpointBoundary";

    private Coroutine drawDebugBoundaryCoroutine;
    private AnimatedBody animatedBody;

    private bool unlocked;

    public void CheckpointUnlockedEffect() {
        animatedBody.PopIn();
    }

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

    [ContextMenu("Update Checkpoint Boundary")]
    private void GenerateBoundary() {
        CheckpointBoundary checkpointBoundary = GetCheckpointBoundary();
        checkpointBoundary.UpdateBoundary(transform, GetRaycastHitUp(), GetRaycastHitDown());
    }

    private CheckpointBoundary GetCheckpointBoundary() {
        if (!string.IsNullOrEmpty(checkPointBoundaryId)) {
            CheckpointBoundary foundCheckpointBounadry = FindObjectIdCheckpointBoundary();

            if(foundCheckpointBounadry != null) {
                return foundCheckpointBounadry;
            } 
        }
        CheckpointBoundary generatedCheckpointBoundary = GenerateCheckpointBoundary(ref checkPointBoundaryId);
        return generatedCheckpointBoundary;
    }

    private CheckpointBoundary FindObjectIdCheckpointBoundary() {
        GameObject foundCheckPointBoundaryGameObject = ObjectId.Find(checkPointBoundaryId);
        if(foundCheckPointBoundaryGameObject == null) { return null; }

        CheckpointBoundary checkpointBoundary = foundCheckPointBoundaryGameObject.GetComponent<CheckpointBoundary>();
        return checkpointBoundary;
    }

    private CheckpointBoundary GenerateCheckpointBoundary(ref string id) {
        CheckpointBoundary checkPointBoundaryPrefab = Resources.Load<CheckpointBoundary>(CHECKPOINT_BOUNDARY_PREFAB_PATH);
        CheckpointBoundary checkpointBoundary = Instantiate(checkPointBoundaryPrefab);
        ObjectId checkpointBoundaryObjectId = checkpointBoundary.gameObject.GetComponent<ObjectId>();
        id = checkpointBoundaryObjectId.GenerateId();
#if UNITY_EDITOR
        EditorGUIUtility.systemCopyBuffer = checkpointBoundaryObjectId.Id;
#endif
        return checkpointBoundary;
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
        CheckpointBoundary foundCheckpointBoundary = FindObjectIdCheckpointBoundary();

        if (foundCheckpointBoundary == null) {
            Debug.LogWarning(name + " Found no reference to checkpoint boundary", this);
        }

        animatedBody = GetComponent<AnimatedBody>();
        animatedBody.OriginalScale = transform.localScale;
        transform.localScale = Vector2.zero;
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