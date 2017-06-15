using IoCPlus;
using UnityEngine;

public class CameraView : View, ICamera {

    public Vector2 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 StartPosition { get { return startPosition; } }
    public CameraBounds CameraBounds { get { return cameraBounds; } }

    [SerializeField] private float pcCameraSize = 10;
    [SerializeField] private float mobileCameraSize = 15;

    [Inject] private Ref<ICamera> cameraRef;

    private new Camera camera;
    private Vector3 startPosition;
    private CameraBounds cameraBounds;

    public override void Initialize() {
        cameraRef.Set(this);
    }

    public void SetCameraBounds(CameraBounds cameraBounds) {
        this.cameraBounds = cameraBounds;

        cameraBounds.CameraHeightOffset = camera.orthographicSize;
        cameraBounds.CameraWidthOffset = cameraBounds.CameraHeightOffset * camera.aspect;
    }

    private void Awake() {
        camera = GetComponent<Camera>();
        startPosition = transform.position;

        if (PlatformHelper.PlatformIsMobile) {
            camera.orthographicSize = mobileCameraSize;
        } else {
            camera.orthographicSize = pcCameraSize;
        }
    }
}
