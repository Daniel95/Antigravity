using IoCPlus;

public class CharacterBounceCommand : Command {

    [Inject] private Ref<ICharacterBounce> characterBounceRef;

    [InjectParameter] private CharacterBounceEvent.Parameter characterBounceParameter;

    protected override void Execute() {
        characterBounceRef.Get().Bounce(characterBounceParameter);
    }
}
