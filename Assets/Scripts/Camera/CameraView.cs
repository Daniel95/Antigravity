using IoCPlus;
using UnityEngine;

public class CameraView : View, ICamera {

    public Vector2 WorldPosition { get { return transform.position; } set { transform.position = new Vector3(value.x, value.y, transform.position.z); } }
    public CameraBounds CameraBounds { get { return cameraBounds; } }
    public float OrthographicSize { get { return cameraComponent.orthographicSize; } set { cameraComponent.orthographicSize = value; } }
    public float OrthographicSizeRatio { get { return GetOrthographicSizeRatio(); } }
    public float MaxOrthographicSize { get { return maxOrthographicSize; } }
    public float MinOrthographicSize { get { return minOrthographicSize; } }

    [SerializeField] private float pcDefaultOrthographicSize = 10;
    [SerializeField] private float mobileOrthographicSize = 15;
    [SerializeField] private float maxOrthographicSize = 35;
    [SerializeField] private float minOrthographicSize = 2;
    [SerializeField] private Camera cameraComponent;

    [Inject] private Ref<ICamera> cameraRef;

    private CameraBounds cameraBounds;
    private float defaultOrthographicSize;

    public override void Initialize() {
        cameraRef.Set(this);
    }

    public void SetCameraBounds(CameraBounds cameraBounds) {
        this.cameraBounds = cameraBounds;

        cameraBounds.CameraHeightOffset = OrthographicSize;
        cameraBounds.CameraWidthOffset = cameraBounds.CameraHeightOffset * cameraComponent.aspect;
        Vector2 clampedBoundsWorldPosition = cameraBounds.GetClampedBoundsPosition(transform.position);
        Vector2 clampedBoundsLocalPosition = transform.InverseTransformVector(clampedBoundsWorldPosition);

        transform.localPosition = clampedBoundsLocalPosition;
    }

    public void ResetOrthographicSizeToSystemDefault() {
        if (PlatformHelper.PlatformIsMobile) {
            OrthographicSize = mobileOrthographicSize;
        } else {
            OrthographicSize = pcDefaultOrthographicSize;
        }
    }

    private float GetOrthographicSizeRatio() {
        return OrthographicSize / defaultOrthographicSize;
    }

    private void Awake() {
        ResetOrthographicSizeToSystemDefault();
        defaultOrthographicSize = OrthographicSize;
    }

}