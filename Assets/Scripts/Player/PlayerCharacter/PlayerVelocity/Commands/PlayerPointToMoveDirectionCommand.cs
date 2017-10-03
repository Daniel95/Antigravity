using IoCPlus;
using UnityEngine;

public class PlayerPointToMoveDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;

    protected override void Execute() {
        Vector2 moveDirection = playerVelocityRef.Get().MoveDirection;
        playerDirectionPointerRef.Get().PointToDirection(moveDirection);
    }
}
