using IoCPlus;

public class StartUpdateGrapplingStateCommand : Command {

    [Inject] private Ref<IGrapplingState> grapplingState;

    protected override void Execute() {
        grapplingState.Get().StartUpdateGrapplingState();
    }
}
