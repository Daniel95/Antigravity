using IoCPlus;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCameraView : View, IFollowCamera {

    [SerializeField] private float smoothness = 0.375f;
    [SerializeField] private float pcCameraSize = 10;
    [SerializeField] private float mobileCameraSize = 15;

    [Inject] private Ref<IFollowCamera> followCameraRef;

    private Transform target;
    private CameraBounds cameraBounds;
    private Camera myCamera;
    private float yStartPos;
    private Vector2 velocity;

    public override void Initialize() {
        followCameraRef.Set(this);
    }

    public void SetTarget(Transform target) {
        this.target = target;
        transform.position = new Vector3(target.position.x, target.position.y, yStartPos);
    }

    public void SetCameraBounds(CameraBounds cameraBounds) {
        this.cameraBounds = cameraBounds;

        cameraBounds.CameraHeightOffset = myCamera.orthographicSize;
        cameraBounds.CameraWidthOffset = cameraBounds.CameraHeightOffset * myCamera.aspect;
    }

    private void LateUpdate() {
        if (target == null || cameraBounds == null) { return; }

        Vector2 delta = target.position - transform.position;
        Vector2 destination = (Vector2)transform.position + delta;
        Vector2 nextPos = Vector2.SmoothDamp(transform.position, cameraBounds.GetClampedBoundsPosition(destination), ref velocity, smoothness, Mathf.Infinity, Time.deltaTime); 
        transform.position = new Vector3(nextPos.x, nextPos.y, yStartPos);
    }

    private void Awake() {
        myCamera = GetComponent<Camera>();
        yStartPos = transform.position.z;

        if(PlatformHelper.PlatformIsMobile) {
            myCamera.orthographicSize = mobileCameraSize;
        } else {
            myCamera.orthographicSize = pcCameraSize;
        }
    }
}
