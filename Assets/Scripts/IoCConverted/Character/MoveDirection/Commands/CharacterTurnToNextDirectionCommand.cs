using IoCPlus;

public class CharacterTurnToNextDirectionCommand : Command {

    [Inject] private Ref<ICharacterMoveDirection> characterMoveDirectionRef;

    [Inject] private CharacterDirectionParameter directionInfo;

    protected override void Execute() {
        characterMoveDirectionRef.Get().TurnToNextDirection(directionInfo);
    }
}
