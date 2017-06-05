using IoCPlus;

public class DispatchHoldShotEventCommand : Command {

    [Inject] private HoldShotEvent holdShotEvent;

    protected override void Execute() {
        holdShotEvent.Dispatch();
    }
}
