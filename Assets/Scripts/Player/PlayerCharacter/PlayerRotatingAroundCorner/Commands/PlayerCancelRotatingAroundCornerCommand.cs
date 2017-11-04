using IoCPlus;

public class PlayerCancelRotatingAroundCornerCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterRotateAroundCorner> playerRotateAroundCornerRef;

    protected override void Execute() {
        playerRotateAroundCornerRef.Get().CancelRotatingAroundCorner();
    }

}
