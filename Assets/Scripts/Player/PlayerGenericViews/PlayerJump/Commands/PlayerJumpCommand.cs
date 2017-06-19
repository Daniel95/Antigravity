using IoCPlus;
using UnityEngine;

public class PlayerJumpCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterJump> playerJumpRef;

    [Inject] private PlayerStatus playerStatus;

    [Inject] private PlayerTemporarySpeedChangeEvent playerTemporarySpeedChangeEvent;
    [Inject] private PlayerSetMoveDirectionEvent playerSetMoveDirectionEvent;
    [Inject] private PlayerRemoveCollisionDirectionEvent playerRemoveCollisionDirectionEvent;

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;

    protected override void Execute() {
        Vector2 newDirection = playerVelocityRef.Get().MoveDirection;
        Vector2 collisionDirection = playerCollisionDirectionRef.Get().GetCurrentCollisionDirection();
        Vector2 centerRaycastDirection = playerRaycastDirectionRef.Get().CenterRaycastDirection();

        if (collisionDirection.x != 0) {
            newDirection.x = collisionDirection.x * -1;
        } else if (collisionDirection.y != 0) {
            newDirection.y = collisionDirection.y * -1;
        }

        if (centerRaycastDirection.x == 0 || centerRaycastDirection.y == 0) {
            playerStatus.Player.transform.position += (Vector3)(newDirection * playerJumpRef.Get().InstantJumpStrength);
        }

        playerSetMoveDirectionEvent.Dispatch(newDirection);
        playerRemoveCollisionDirectionEvent.Dispatch(collisionDirection);
        playerTemporarySpeedChangeEvent.Dispatch(new PlayerTemporarySpeedChangeEvent.Parameter(0.5f + playerJumpRef.Get().JumpSpeedBoost));
    }
}
