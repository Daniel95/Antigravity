using IoCPlus;

public class CharacterSetMoveDirectionToVelocityDirectionCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        characterVelocityRef.Get().MoveDirection = characterVelocityRef.Get().GetVelocityDirection();
    }
}
