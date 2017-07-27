using IoCPlus;

public class EnableCameraZoomInputCommand : Command<bool> {

    [Inject] private CameraStatus cameraStatus;

    protected override void Execute(bool enable) {
        cameraStatus.ZoomInput = enable;
    }

}
