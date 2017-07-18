using IoCPlus;
using UnityEngine;

public class DispatchPlayerTurnToNextDirectionEventCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    [Inject] private PlayerTurnToNextDirectionEvent playerTurnToNextDirectionEvent;

    protected override void Execute() {
        Vector2 ceilPreviousVelocityDirection = playerVelocityRef.Get().GetCeilPreviousVelocityDirection();
        Vector2 surroundingsDirection = SurroundingDirectionHelper.GetSurroundingsDirection(playerCollisionDirectionRef.Get(), playerRaycastDirectionRef.Get());

        playerTurnToNextDirectionEvent.Dispatch(new PlayerTurnToNextDirectionEvent.Parameter(
            ceilPreviousVelocityDirection,
            surroundingsDirection
        ));
    }
}
