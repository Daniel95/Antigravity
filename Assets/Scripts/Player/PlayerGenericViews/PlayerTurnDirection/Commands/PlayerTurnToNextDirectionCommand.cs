using IoCPlus;
using UnityEngine;

public class PlayerTurnToNextDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerMoveDirectionRef;

    [InjectParameter] private PlayerTurnToNextDirectionEvent.Parameter playerTurnToNextDirectionParameter;

    protected override void Execute() {
        Vector2 moveDirection = playerTurnToNextDirectionParameter.MoveDirection;
        Vector2 surroundingsDirection = playerTurnToNextDirectionParameter.SurroundingsDirection;

        playerMoveDirectionRef.Get().TurnToNextDirection(moveDirection, surroundingsDirection);
    }
}
