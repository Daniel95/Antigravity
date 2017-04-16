using IoCPlus;

public class CharacterExitKillingTriggerCommand : Command {

    [Inject] private Ref<ICharacterDie> characterJumpRef;

    [InjectParameter] private string killerTag;

    protected override void Execute() {
        characterJumpRef.Get().ExitingKillingTrigger(killerTag);
    }
}
