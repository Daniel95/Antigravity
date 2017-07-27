using IoCPlus;

public class AbortIfCameraZoomInputIsFalseCommand : Command {

    [Inject] private CameraStatus cameraStatus;

    protected override void Execute() {
        if(!cameraStatus.ZoomInput) {
            Abort();
        }
    }

}
