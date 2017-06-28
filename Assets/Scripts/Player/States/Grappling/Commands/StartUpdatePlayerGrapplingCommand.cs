using IoCPlus;

public class StartUpdatePlayerGrapplingCommand : Command {

    [Inject] private Ref<IPlayerGrappling> grapplingState;

    protected override void Execute() {
        grapplingState.Get().StartUpdateGrapplingState();
    }
}
