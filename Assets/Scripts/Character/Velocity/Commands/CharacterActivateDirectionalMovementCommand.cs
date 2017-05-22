using IoCPlus;

public class CharacterActivateDirectionalMovementCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    protected override void Execute() {
        controlVelocityRef.Get().EnableDirectionalMovement(true);
    }
}
