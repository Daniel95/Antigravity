using IoCPlus;

public class CharacterTurnToNextDirectionCommand : Command {

    [Inject] private Ref<ICharacterMoveDirection> characterMoveDirectionRef;

    [Inject] private CharacterTurnToNextDirectionParameter characterTurnToNextDirectionParameter;

    protected override void Execute() {
        characterMoveDirectionRef.Get().TurnToNextDirection(characterTurnToNextDirectionParameter);
    }
}
