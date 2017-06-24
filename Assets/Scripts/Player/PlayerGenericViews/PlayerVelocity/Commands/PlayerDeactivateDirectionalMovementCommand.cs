using IoCPlus;

public class PlayerDeactivateDirectionalMovementCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerVelocityRef.Get().DisableDirectionalMovement();
    }
}
