using IoCPlus;

public class PlayerActivateDirectionalMovementCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerVelocityRef.Get().EnableDirectionalMovement(true);
    }
}
