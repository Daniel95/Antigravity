using IoCPlus;

public class CharacterBounceCommand : Command {

    [Inject] private Ref<ICharacterBounce> characterBounceRef;

    [InjectParameter] private CharacterBounceParameter directionParameter;

    protected override void Execute() {
        characterBounceRef.Get().Bounce(directionParameter);
    }
}
