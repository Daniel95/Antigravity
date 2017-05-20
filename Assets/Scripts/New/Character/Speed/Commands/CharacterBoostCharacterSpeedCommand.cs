using IoCPlus;

public class CharacterBoostCharacterSpeedCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    [InjectParameter] private CharacterBoostSpeedEvent.Parameter characterBoostSpeedParameter;

    protected override void Execute() {
        characterVelocityRef.Get().SetSpeed(characterBoostSpeedParameter.NewSpeed);
        characterVelocityRef.Get().StartReturnSpeedToOriginal(characterBoostSpeedParameter.ReturnSpeed);
    }
}
