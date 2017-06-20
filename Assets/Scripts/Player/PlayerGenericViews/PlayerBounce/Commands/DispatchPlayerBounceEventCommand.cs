using IoCPlus;
using UnityEngine;

public class DispatchPlayerBounceEventCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    [Inject] private PlayerBounceEvent playerBounceEvent;

    protected override void Execute() {
        Vector2 surroundingsDirection = SurroundingDirectionHelper.GetSurroundingsDirection(playerCollisionDirectionRef.Get(), playerRaycastDirectionRef.Get());

        playerBounceEvent.Dispatch(new PlayerBounceEvent.Parameter(
            playerVelocityRef.Get().MoveDirection,
            surroundingsDirection
        ));
    }
}
