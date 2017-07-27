using IoCPlus;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraView : View, ICamera {

    public Vector2 Position { get { return transform.position; } set { transform.position = value; } }
    public CameraBounds CameraBounds { get { return cameraBounds; } }

    [SerializeField] private float pcCameraSize = 10;
    [SerializeField] private float mobileCameraSize = 15;

    [Inject] private Ref<ICamera> cameraRef;

    private Camera cameraComponent;
    private CameraBounds cameraBounds;

    public override void Initialize() {
        cameraRef.Set(this);
    }

    public void SetCameraBounds(CameraBounds cameraBounds) {
        this.cameraBounds = cameraBounds;

        cameraBounds.CameraHeightOffset = cameraComponent.orthographicSize;
        cameraBounds.CameraWidthOffset = cameraBounds.CameraHeightOffset * cameraComponent.aspect;
        Vector2 clampedBoundsPosition = cameraBounds.GetClampedBoundsPosition(transform.position);
        Vector2 localClampedBoundsPosition = transform.InverseTransformPoint(clampedBoundsPosition);

        Debug.Log("setbounds");
        transform.localPosition = localClampedBoundsPosition;
    }

    private void Awake() {
        cameraComponent = GetComponent<Camera>();

        if (PlatformHelper.PlatformIsMobile) {
            cameraComponent.orthographicSize = mobileCameraSize;
        } else {
            cameraComponent.orthographicSize = pcCameraSize;
        }
    }

}
