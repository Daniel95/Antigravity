using IoCPlus;
using UnityEngine;

public class DispatchCharacterTurnToNextDirectionEventCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterSurroundingDirection> characterSurroundingDirection;
    [Inject] private Ref<ICharacterRaycastDirection> characterRaycastRef;

    [Inject] private CharacterTurnToNextDirectionEvent characterTurnToNextDirectionEvent;

    protected override void Execute() {
        characterTurnToNextDirectionEvent.Dispatch(new CharacterTurnToNextDirectionEvent.Parameter(
            characterVelocityRef.Get().GetCeilPreviousVelocityDirection(),
            characterSurroundingDirection.Get().GetSurroundingsDirection(true, false, true),
            characterRaycastRef.Get().GetCornersDirection()
        ));
    }
}
