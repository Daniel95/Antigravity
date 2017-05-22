using IoCPlus;

public class DispatchHookPullBackEventCommand : Command {

    [Inject] private PullBackHookEvent hookPullBackEvent;

    protected override void Execute() {
        hookPullBackEvent.Dispatch();
    }
}
