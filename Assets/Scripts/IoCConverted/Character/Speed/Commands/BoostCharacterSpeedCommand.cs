using IoCPlus;

public class BoostCharacterSpeedCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    [InjectParameter] private BoostSpeedParameter boostSpeedInfo;

    protected override void Execute() {
        characterVelocityRef.Get().SetSpeed(boostSpeedInfo.NewSpeed);
        characterVelocityRef.Get().StartReturnSpeedToOriginal(boostSpeedInfo.ReturnSpeed);
    }
}
