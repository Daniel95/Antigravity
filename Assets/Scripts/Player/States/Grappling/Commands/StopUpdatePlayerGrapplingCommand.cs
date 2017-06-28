using IoCPlus;

public class StopUpdatePlayerGrapplingCommand : Command {

    [Inject] private Ref<IPlayerGrappling> grapplingState;

    protected override void Execute() {
        grapplingState.Get().StopUpdateGrapplingState();
    }
}
