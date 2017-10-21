using IoCPlus;
using UnityEngine;

public class ZoomCameraCommand : Command {

    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    [InjectParameter] private Vector2 position;
    [InjectParameter] private float delta;

    protected override void Execute() {
        cameraVelocityRef.Get().Zoom(position, delta);
    }

}
