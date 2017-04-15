using IoCPlus;

public class CharacterJumpCommand : Command {

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    [InjectParameter] private CharacterJumpParameter

    protected override void Execute() {
        characterJumpRef.Get().Jump(new CharacterJumpParameter);
    }
}
