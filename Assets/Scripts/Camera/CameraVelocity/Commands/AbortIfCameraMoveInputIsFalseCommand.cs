using IoCPlus;

public class AbortIfCameraMoveInputIsFalseCommand : Command {

    [Inject] private CameraStatus cameraStatus;

    protected override void Execute() {
        if(!cameraStatus.MoveInput) {
            Abort();
        }
    }

}
