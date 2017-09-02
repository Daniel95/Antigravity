using IoCPlus;

public class ZoomCameraCommand : Command {

    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    [InjectParameter] private float delta;

    protected override void Execute() {
        cameraVelocityRef.Get().Zoom(-delta);
    }

}
