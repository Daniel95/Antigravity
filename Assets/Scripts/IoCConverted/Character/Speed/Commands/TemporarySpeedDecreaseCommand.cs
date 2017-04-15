using IoCPlus;

public class TemporarySpeedDecreaseCommand : Command {

    [Inject] private Ref<ICharacterSpeed> characterSpeedRef;

    [Inject] private TemporarySpeedChangeEvent temporarySpeedChangeEvent;

    protected override void Execute() {
        temporarySpeedChangeEvent.Dispatch(new TemporarySpeedChangeParameter(0.5f - characterSpeedRef.Get().SpeedBoostValue));
    }
}
