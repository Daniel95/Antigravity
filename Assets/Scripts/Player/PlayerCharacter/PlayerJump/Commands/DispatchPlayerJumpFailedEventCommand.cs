using IoCPlus;

public class DispatchPlayerJumpFailedEventCommand : Command {

    [Inject] private PlayerJumpFailedEvent playerJumpFailedEvent;

    protected override void Execute() {
        playerJumpFailedEvent.Dispatch();
    }

}
