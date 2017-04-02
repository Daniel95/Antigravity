using IoCPlus;

public class PlayerJumpCommand : Command {

    [Inject] private Ref<ISlidingState> slidingState;

    protected override void Execute() {
        slidingState.Get().Jump();
    }
}
