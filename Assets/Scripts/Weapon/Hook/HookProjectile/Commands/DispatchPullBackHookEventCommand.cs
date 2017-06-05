using IoCPlus;

public class DispatchPullBackHookEventCommand : Command {

    [Inject] private PullBackHookEvent pullBackHookEvent;

    protected override void Execute() {
        pullBackHookEvent.Dispatch();
    }
}
