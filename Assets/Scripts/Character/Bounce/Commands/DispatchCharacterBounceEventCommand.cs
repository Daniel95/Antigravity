using IoCPlus;

public class DispatchCharacterBounceEventCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterSurroundingDirection> characterSurroundingsDirection;

    [Inject] private CharacterBounceEvent characterBounceEvent;

    protected override void Execute() {
        characterBounceEvent.Dispatch(new CharacterBounceEvent.Parameter(
            characterVelocityRef.Get().Direction,
            characterSurroundingsDirection.Get().GetSurroundingsDirection()
        ));
    }
}
