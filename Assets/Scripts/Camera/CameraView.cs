using IoCPlus;
using UnityEngine;

public class CameraView : View, ICamera {

    public Vector2 Position { get { return transform.position; } set { transform.position = value; } }

    [Inject] private Ref<ICamera> cameraRef;

    public override void Initialize() {
        cameraRef.Set(this);
    }
}
