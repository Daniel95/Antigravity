using IoCPlus;

public class CharacterBoostCharacterSpeedCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    [InjectParameter] private CharacterBoostSpeedParameter boostSpeedInfo;

    protected override void Execute() {
        characterVelocityRef.Get().SetSpeed(boostSpeedInfo.NewSpeed);
        characterVelocityRef.Get().StartReturnSpeedToOriginal(boostSpeedInfo.ReturnSpeed);
    }
}
