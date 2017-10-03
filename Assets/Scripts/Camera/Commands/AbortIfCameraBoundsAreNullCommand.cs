using IoCPlus;

public class AbortIfCameraBoundsAreNullCommand : Command {

    [Inject] private Ref<ICamera> cameraRef;

    protected override void Execute() {
        if (CameraBounds.Instance == null) {
            Abort();
        }
    }

}
