using IoCPlus;

public class PlayerEnableDirectionalMovementCommand : Command<bool> {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute(bool enable) {
        playerVelocityRef.Get().EnableDirectionalMovement(enable);
    }
}
