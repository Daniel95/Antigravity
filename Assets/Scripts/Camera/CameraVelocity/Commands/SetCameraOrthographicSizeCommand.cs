using IoCPlus;

public class SetCameraOrthographicSizeCommand : Command<int> {

    [Inject] private Ref<ICamera> cameraRef;

    protected override void Execute(int orthographicSize) {
        cameraRef.Get().OrthographicSize = orthographicSize;
    }

}
