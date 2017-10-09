using IoCPlus;

public class SetCameraMoveInputTypeCommand : Command<CameraMoveInputType> {

    [Inject] private CameraStatus cameraStatus;

    protected override void Execute(CameraMoveInputType cameraMoveInputType) {
        cameraStatus.CameraMoveInputType = cameraMoveInputType;
    }

}
