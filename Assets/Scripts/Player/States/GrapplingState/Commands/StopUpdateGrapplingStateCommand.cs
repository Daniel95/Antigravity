using IoCPlus;

public class StopUpdateGrapplingStateCommand : Command {

    [Inject] private Ref<IGrapplingState> grapplingState;

    protected override void Execute() {
        grapplingState.Get().StopUpdateGrapplingState();
    }
}
