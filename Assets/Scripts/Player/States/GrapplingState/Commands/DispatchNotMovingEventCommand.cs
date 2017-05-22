using IoCPlus;

public class DispatchNotMovingEventCommand : Command {

    [Inject] private NotMovingEvent notMovingEvent;

    protected override void Execute() {
        notMovingEvent.Dispatch();
    }
}
