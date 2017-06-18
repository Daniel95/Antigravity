using IoCPlus;

public class CharacterTemporarySpeedDecreaseCommand : Command {

    [Inject] private Ref<ICharacterSpeed> characterSpeedRef;

    [Inject] private PlayerTemporarySpeedChangeEvent temporarySpeedChangeEvent;

    protected override void Execute() {
        temporarySpeedChangeEvent.Dispatch(new PlayerTemporarySpeedChangeEvent.Parameter(0.5f - characterSpeedRef.Get().SpeedBoostValue));
    }
}
