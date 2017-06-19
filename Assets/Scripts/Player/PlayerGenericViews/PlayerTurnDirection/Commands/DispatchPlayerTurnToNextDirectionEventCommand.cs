using IoCPlus;
using UnityEngine;

public class DispatchPlayerTurnToNextDirectionEventCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterSurroundingDirection> playerSurroundingDirection;
    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastRef;

    [Inject] private PlayerTurnToNextDirectionEvent playerTurnToNextDirectionEvent;

    protected override void Execute() {
        playerTurnToNextDirectionEvent.Dispatch(new PlayerTurnToNextDirectionEvent.Parameter(
            playerVelocityRef.Get().GetCeilPreviousVelocityDirection(),
            playerSurroundingDirection.Get().GetSurroundingsDirection(true, false, true),
            playerRaycastRef.Get().GetCornersDirection()
        ));
    }
}
