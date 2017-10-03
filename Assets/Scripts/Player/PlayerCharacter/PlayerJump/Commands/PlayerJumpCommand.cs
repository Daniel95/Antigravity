using IoCPlus;
using UnityEngine;

public class PlayerJumpCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterJump> playerJumpRef;

    [Inject] private PlayerStatus playerStatus;

    [Inject] private PlayerTemporarySpeedChangeEvent playerTemporarySpeedChangeEvent;
    [Inject] private PlayerSetMoveDirectionEvent playerSetMoveDirectionEvent;

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;

    protected override void Execute() {
        Vector2 newDirection = playerVelocityRef.Get().MoveDirection;

        Vector2 surroundingsDirection = SurroundingDirectionHelper.GetSurroundingsDirection(playerCollisionDirectionRef.Get(), playerRaycastDirectionRef.Get());

        if (surroundingsDirection.x != 0) {
            newDirection.x = surroundingsDirection.x * -1;
        } else if (surroundingsDirection.y != 0) {
            newDirection.y = surroundingsDirection.y * -1;
        }

        if (surroundingsDirection.x == 0 || surroundingsDirection.y == 0) {
            playerStatus.Player.transform.position += (Vector3)(newDirection * playerJumpRef.Get().InstantJumpStrength);
        }

        playerSetMoveDirectionEvent.Dispatch(newDirection);
        playerTemporarySpeedChangeEvent.Dispatch(new PlayerTemporarySpeedChangeEvent.Parameter(0.5f + playerJumpRef.Get().JumpSpeedBoost));
    }
}
