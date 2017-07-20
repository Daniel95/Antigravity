using IoCPlus;

public class DispatchPlayerTurnToNextDirectionEventCommand : Command {

    [Inject] private PlayerTurnToNextDirectionEvent playerTurnToNextDirectionEvent;

    protected override void Execute() {
        playerTurnToNextDirectionEvent.Dispatch();
    }
}
