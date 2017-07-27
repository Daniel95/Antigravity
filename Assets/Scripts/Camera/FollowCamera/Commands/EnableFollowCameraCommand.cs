using IoCPlus;

public class EnableFollowCameraCommand : Command<bool> {

    [Inject] private Ref<IFollowCamera> followCameraRef;

    protected override void Execute(bool enable) {
        followCameraRef.Get().EnableFollowCamera(enable);
    }
}
