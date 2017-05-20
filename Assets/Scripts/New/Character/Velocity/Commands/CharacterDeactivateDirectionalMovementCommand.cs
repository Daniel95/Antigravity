using IoCPlus;

public class CharacterDeactivateDirectionalMovementCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    protected override void Execute() {
        controlVelocityRef.Get().EnableDirectionalMovement(false);
    }
}
