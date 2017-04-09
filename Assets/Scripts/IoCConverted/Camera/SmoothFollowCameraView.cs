using UnityEngine;
using IoCPlus;

public class SmoothFollowCameraView : View {

    [Inject] private PlayerModel playerModel;

    [SerializeField]
    private float smoothness = 0.375f;

    private Transform target;

    private float _yStartPos;

    private Vector2 _velocity;

    private BoundsCamera _boundsCamera;

    void Awake() {
        _boundsCamera = GetComponent<BoundsCamera>();

        _yStartPos = transform.position.z;
    }

    public override void Initialize() {
        base.Initialize();
        target = playerModel.player.transform;
        transform.position = new Vector3(target.position.x, target.position.y, _yStartPos);
    }

    void LateUpdate() {
        Vector2 delta = target.position - transform.position;
        Vector2 destination = (Vector2)transform.position + delta;
        Vector2 nextPos = Vector2.SmoothDamp(transform.position, _boundsCamera.GetBoundsPosition(destination), ref _velocity, smoothness, Mathf.Infinity, Time.deltaTime); 
        transform.position = new Vector3(nextPos.x, nextPos.y, _yStartPos);
    }
}
