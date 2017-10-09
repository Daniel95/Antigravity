using IoCPlus;

public class AbortIfCameraMoveInputTypeIsNotCommand : Command<CameraMoveInputType> {

    [Inject] private CameraStatus cameraStatus;

    protected override void Execute(CameraMoveInputType cameraMoveInputType) {
        if(cameraStatus.CameraMoveInputType != cameraMoveInputType) {
            Abort();
        }
    }

}
