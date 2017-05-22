using IoCPlus;

public class CharacterEnableDirectionalMovementCommand : Command<bool> {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute(bool enable) {
        characterVelocityRef.Get().EnableDirectionalMovement(enable);
    }
}
