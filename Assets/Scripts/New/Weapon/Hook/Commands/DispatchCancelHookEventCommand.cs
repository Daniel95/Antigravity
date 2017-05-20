using IoCPlus;

public class DispatchCancelHookEventCommand : Command {

    [Inject] private CancelHookEvent cancelHookEvent;

    protected override void Execute() {
        cancelHookEvent.Dispatch();
    }
}
