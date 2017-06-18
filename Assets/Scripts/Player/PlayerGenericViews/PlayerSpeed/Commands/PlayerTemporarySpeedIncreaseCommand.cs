using IoCPlus;

public class PlayerTemporarySpeedIncreaseCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterSpeed> playerSpeedRef;

    [Inject] private PlayerTemporarySpeedChangeEvent playerTemporarySpeedChangeEvent;

    protected override void Execute() {
        playerTemporarySpeedChangeEvent.Dispatch(new PlayerTemporarySpeedChangeEvent.Parameter(0.5f + playerSpeedRef.Get().SpeedBoostValue));
    }
}
