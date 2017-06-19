using IoCPlus;

public class PlayerBoostCharacterSpeedCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    [InjectParameter] private PlayerBoostSpeedEvent.Parameter characterBoostSpeedParameter;

    protected override void Execute() {
        playerVelocityRef.Get().SetSpeed(characterBoostSpeedParameter.NewSpeed);
        playerVelocityRef.Get().StartReturnSpeedToOriginal(characterBoostSpeedParameter.ReturnSpeed);
    }
}
