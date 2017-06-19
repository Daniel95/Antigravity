using IoCPlus;

public class PlayerSetSavedDirectionToStartDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerMoveDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerMoveDirectionRef.Get().SavedDirection = playerVelocityRef.Get().MoveDirection;
    }
}
