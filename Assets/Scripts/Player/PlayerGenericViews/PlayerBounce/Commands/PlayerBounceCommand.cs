using IoCPlus;
using UnityEngine;

public class PlayerBounceCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    [Inject] private PlayerSetMoveDirectionEvent playerSetMoveDirectionEvent;

    [InjectParameter] private PlayerBounceEvent.Parameter playerBounceParameter;

    protected override void Execute() {
        if (playerBounceParameter.CollisionDirection.x != 0) {
            playerBounceParameter.MoveDirection.x *= -1;
        }
        if (playerBounceParameter.CollisionDirection.y != 0) {
            playerBounceParameter.MoveDirection.y *= -1;
        }

        playerSetMoveDirectionEvent.Dispatch(playerBounceParameter.MoveDirection);
    }
}
