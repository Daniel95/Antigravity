using IoCPlus;

public class CharacterTurnToNextDirectionCommand : Command {

    [Inject] private Ref<ICharacterTurnDirection> characterMoveDirectionRef;

    [InjectParameter] private CharacterTurnToNextDirectionEvent.Parameter characterTurnToNextDirectionParameter;

    protected override void Execute() {
        characterMoveDirectionRef.Get().TurnToNextDirection(characterTurnToNextDirectionParameter);
    }
}
