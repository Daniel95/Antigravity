using IoCPlus;

public class PlayerResetCollisionsCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    protected override void Execute() {
        playerCollisionDirectionRef.Get().ResetCollisions();
    }
}
