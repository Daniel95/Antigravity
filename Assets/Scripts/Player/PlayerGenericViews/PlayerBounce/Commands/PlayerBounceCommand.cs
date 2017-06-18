using IoCPlus;

public class PlayerBounceCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterBounce> playerBounceRef;

    [InjectParameter] private PlayerBounceEvent.Parameter playerBounceParameter;

    protected override void Execute() {
        playerBounceRef.Get().Bounce(playerBounceParameter);
    }
}
