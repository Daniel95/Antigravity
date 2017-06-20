using IoCPlus;
using UnityEngine;

public class DispatchPlayerTurnToNextDirectionEventCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    [Inject] private PlayerTurnToNextDirectionEvent playerTurnToNextDirectionEvent;

    protected override void Execute() {
        Vector2 surroundingsDirection = SurroundingDirectionHelper.GetSurroundingsDirection(playerCollisionDirectionRef.Get(), playerRaycastDirectionRef.Get(), true, false, true);

        playerTurnToNextDirectionEvent.Dispatch(new PlayerTurnToNextDirectionEvent.Parameter(
            playerVelocityRef.Get().GetCeilPreviousVelocityDirection(),
            surroundingsDirection,
            playerRaycastDirectionRef.Get().GetCornersDirection()
        ));
    }
}
