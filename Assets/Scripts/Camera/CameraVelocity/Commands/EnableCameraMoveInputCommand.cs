using IoCPlus;

public class EnableCameraMoveInputCommand : Command<bool> {

    [Inject] private CameraStatus cameraStatus;

    protected override void Execute(bool enable) {
        cameraStatus.MoveInput = enable;
    }

}
