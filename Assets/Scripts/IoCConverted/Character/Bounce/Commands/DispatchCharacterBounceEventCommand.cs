using IoCPlus;

public class DispatchCharacterBounceEventCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirection;
    [Inject] private Ref<ICharacterRaycast> characterRaycastRef;

    [Inject] private CharacterBounceEvent characterBounceEvent;

    protected override void Execute() {
        characterBounceEvent.Dispatch(new CharacterBounceEvent.Parameter(
            characterVelocityRef.Get().MoveDirection,
            characterCollisionDirection.Get().GetCurrentCollisionDirection(characterRaycastRef.Get().GetCornersDirection())
        ));
    }
}
