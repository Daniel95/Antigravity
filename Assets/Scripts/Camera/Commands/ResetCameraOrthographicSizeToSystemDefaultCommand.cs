using IoCPlus;

public class ResetCameraOrthographicSizeToSystemDefaultCommand : Command {

    [Inject] private Ref<ICamera> cameraRef;

    protected override void Execute() {
        cameraRef.Get().ResetOrthographicSizeToSystemDefault();
    }

}
