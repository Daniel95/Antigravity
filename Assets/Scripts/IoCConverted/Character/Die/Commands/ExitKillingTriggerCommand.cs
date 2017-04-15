using IoCPlus;

public class ExitKillingTriggerCommand : Command {

    [Inject] private Ref<ICharacterDie> characterJumpRef;

    [InjectParameter] private string killerTag;

    protected override void Execute() {
        characterJumpRef.Get().ExitingKillingTrigger(killerTag);
    }
}
