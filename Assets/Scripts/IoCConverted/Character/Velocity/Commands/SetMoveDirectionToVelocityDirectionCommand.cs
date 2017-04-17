using IoCPlus;

public class SetMoveDirectionToVelocityDirectionCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        characterVelocityRef.Get().Direction = characterVelocityRef.Get().GetVelocityDirection();
    }
}
