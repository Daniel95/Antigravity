using IoCPlus;

public class DispatchCharacterTurnToNextDirectionEventCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;
    [Inject] private Ref<ICharacterRaycast> characterRayCastRef;

    [Inject] private CharacterTurnToNextDirectionEvent characterTurnToNextDirectionEvent;

    protected override void Execute() {
        characterTurnToNextDirectionEvent.Dispatch(new CharacterTurnToNextDirectionEvent.Parameter(
            characterVelocityRef.Get().MoveDirection,
            characterCollisionDirectionRef.Get().GetCurrentCollisionDirection(),
            characterRayCastRef.Get().GetCornersDirection()
        ));
    }
}
