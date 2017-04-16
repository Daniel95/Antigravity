using IoCPlus;

public class CharacterSetSavedDirectionToStartDirectionCommand : Command {

    [Inject] private Ref<ICharacterMoveDirection> characterMoveDirectionRef;
    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        characterMoveDirectionRef.Get().SavedDirection = characterVelocityRef.Get().Direction;
    }
}
