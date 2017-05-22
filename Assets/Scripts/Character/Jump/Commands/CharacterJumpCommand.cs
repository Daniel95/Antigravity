using IoCPlus;

public class CharacterJumpCommand : Command {

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    [Inject] private CharacterRemoveCollisionDirectionEvent characterRemoveCollisionDirectionEvent;

    [InjectParameter] private CharacterJumpEvent.Parameter characterJumpParameter;

    protected override void Execute() {
        characterJumpRef.Get().Jump(characterJumpParameter);
    }
}
