using IoCPlus;
using UnityEngine;

public class CameraBounds : View {

    public float CameraHeightOffset { get { return cameraHeightOffset; } set { cameraHeightOffset = value; } }
    public float CameraWidthOffset { get { return cameraWidthOffset; } set { cameraWidthOffset = value; } }

    [SerializeField] private Transform rightBound;
    [SerializeField] private Transform leftbound;
    [SerializeField] private Transform upBound;
    [SerializeField] private Transform downBound;

    private float cameraHeightOffset;
    private float cameraWidthOffset;

    public Vector2 GetClampedBoundsPosition(Vector2 position) {
        return new Vector2(Mathf.Clamp(position.x, leftbound.position.x + cameraWidthOffset, rightBound.position.x - cameraWidthOffset), Mathf.Clamp(position.y, downBound.position.y + cameraHeightOffset, upBound.position.y - cameraHeightOffset));
    }

}
