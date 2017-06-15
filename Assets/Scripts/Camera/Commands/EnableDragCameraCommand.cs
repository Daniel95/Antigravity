using IoCPlus;

public class EnableDragCameraCommand : Command<bool> {

    [Inject] private Ref<IDragCamera> dragCameraRef;

    protected override void Execute(bool enable) {
        dragCameraRef.Get().EnableDragCamera(enable);
    }
}
