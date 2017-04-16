using IoCPlus;

public class CharacterTemporarySpeedChangeCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    [Inject] private Ref<ICharacterSpeed> characterSpeedRef;

    [Inject] private CharacterBoostSpeedEvent boostSpeedEvent;

    [InjectParameter] private CharacterTemporarySpeedChangeParameter temporarySpeedChangeParameter;

    /// <summary>
    /// Changes speed by a specified amount, below 0.5 is slower, above 0.5 is faster
    /// never sets speed lower then minimum
    /// </summary>
    protected override void Execute() {

        ICharacterSpeed characterSpeed = characterSpeedRef.Get();

        if (characterSpeed.ChangeSpeedCDCounter < 0) {
            characterSpeed.StartChangeSpeedCdCounter();
        } else {
            characterSpeed.ChangeSpeedCDCounter = characterSpeed.ChangeSpeedCDStartValue;
        }

        float newSpeed = characterSpeed.CalculateNewSpeed(characterVelocityRef.Get().CurrentSpeed, temporarySpeedChangeParameter.Amount, temporarySpeedChangeParameter.NeutralValue);

        if (newSpeed - characterVelocityRef.Get().CurrentSpeed > characterSpeed.MaxSpeedChange) {
            newSpeed = characterVelocityRef.Get().CurrentSpeed + characterSpeed.MaxSpeedChange;
        }

        //keep the speeds in bounds, above originalspeed, below the speed muliplier
        if (newSpeed > characterVelocityRef.Get().OriginalSpeed * characterSpeed.MaxSpeedMultiplier) {
            newSpeed = characterVelocityRef.Get().OriginalSpeed * characterSpeed.MaxSpeedMultiplier;
        } else if (newSpeed < characterVelocityRef.Get().OriginalSpeed) {
            newSpeed = characterVelocityRef.Get().OriginalSpeed;
        }

        boostSpeedEvent.Dispatch(new CharacterBoostSpeedParameter(newSpeed, characterSpeed.ReturnSpeed));
    }
}
