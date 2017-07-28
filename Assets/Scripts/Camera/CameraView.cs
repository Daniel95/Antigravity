using IoCPlus;
using UnityEngine;

public class CameraView : View, ICamera {

    public Vector2 WorldPosition { get { return transform.position; } set { transform.position = new Vector3(value.x, value.y, transform.position.z); } }
    public CameraBounds CameraBounds { get { return cameraBounds; } }

    [SerializeField] private float pcCameraSize = 10;
    [SerializeField] private float mobileCameraSize = 15;
    [SerializeField] private Camera cameraComponent;

    [Inject] private Ref<ICamera> cameraRef;

    private CameraBounds cameraBounds;

    public override void Initialize() {
        cameraRef.Set(this);
    }

    public void SetCameraBounds(CameraBounds cameraBounds) {
        this.cameraBounds = cameraBounds;

        cameraBounds.CameraHeightOffset = cameraComponent.orthographicSize;
        cameraBounds.CameraWidthOffset = cameraBounds.CameraHeightOffset * cameraComponent.aspect;
        Vector2 clampedBoundsWorldPosition = cameraBounds.GetClampedBoundsPosition(transform.position);
        Vector2 clampedBoundsLocalPosition = transform.InverseTransformVector(clampedBoundsWorldPosition);

        transform.localPosition = clampedBoundsLocalPosition;
    }

    private void Awake() {
        if (PlatformHelper.PlatformIsMobile) {
            cameraComponent.orthographicSize = mobileCameraSize;
        } else {
            cameraComponent.orthographicSize = pcCameraSize;
        }
    }

}