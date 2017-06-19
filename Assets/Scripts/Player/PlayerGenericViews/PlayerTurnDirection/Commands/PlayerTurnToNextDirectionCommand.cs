using IoCPlus;

public class PlayerTurnToNextDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerMoveDirectionRef;

    [InjectParameter] private PlayerTurnToNextDirectionEvent.Parameter playerTurnToNextDirectionParameter;

    protected override void Execute() {
        playerMoveDirectionRef.Get().TurnToNextDirection(playerTurnToNextDirectionParameter);
    }
}
