using IoCPlus;

public class PlayerResetSavedCollisionsCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    protected override void Execute() {
        playerCollisionDirectionRef.Get().ResetSavedCollisions();
    }
}
