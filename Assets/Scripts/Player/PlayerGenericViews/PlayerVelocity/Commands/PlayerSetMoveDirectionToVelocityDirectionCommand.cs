using IoCPlus;

public class PlayerSetMoveDirectionToVelocityDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerVelocityRef.Get().SetMoveDirection(playerVelocityRef.Get().GetVelocityDirection());
    }
}
