using IoCPlus;

public class DispatchTurnFromWallEventCommand : Command {

    [Inject] private TurnFromWallEvent turnFromWallEvent;

    protected override void Execute() {
        turnFromWallEvent.Dispatch();
    }
}
