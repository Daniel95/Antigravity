using IoCPlus;

public class CharacterBounceCommand : Command {

    [Inject] private Ref<ICharacterBounce> characterBounceRef;

    [InjectParameter] private CharacterDirectionParameter directionInfo;

    protected override void Execute() {
        characterBounceRef.Get().Bounce(directionInfo);
    }
}
