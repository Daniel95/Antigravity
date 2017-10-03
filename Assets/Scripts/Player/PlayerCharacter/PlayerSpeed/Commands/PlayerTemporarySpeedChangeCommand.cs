using IoCPlus;

public class PlayerTemporarySpeedChangeCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    [Inject(Label.Player)] private Ref<ICharacterSpeed> playerSpeedRef;

    [Inject] private PlayerBoostSpeedEvent boostSpeedEvent;

    [InjectParameter] private PlayerTemporarySpeedChangeEvent.Parameter temporarySpeedChangeParameter;

    protected override void Execute() {
        ICharacterSpeed playerSpeed = playerSpeedRef.Get();

        if (playerSpeed.ChangeSpeedCDCounter < 0) {
            playerSpeed.StartChangeSpeedCdCounter();
        } else {
            playerSpeed.ChangeSpeedCDCounter = playerSpeed.ChangeSpeedCDStartValue;
        }

        float newSpeed = playerSpeed.CalculateNewSpeed(playerVelocityRef.Get().CurrentSpeed, temporarySpeedChangeParameter.Amount, temporarySpeedChangeParameter.NeutralValue);

        if (newSpeed - playerVelocityRef.Get().CurrentSpeed > playerSpeed.MaxSpeedChange) {
            newSpeed = playerVelocityRef.Get().CurrentSpeed + playerSpeed.MaxSpeedChange;
        }

        //keep the speeds in bounds, above originalspeed, below the speed muliplier
        if (newSpeed > playerVelocityRef.Get().OriginalSpeed * playerSpeed.MaxSpeedMultiplier) {
            newSpeed = playerVelocityRef.Get().OriginalSpeed * playerSpeed.MaxSpeedMultiplier;
        } else if (newSpeed < playerVelocityRef.Get().OriginalSpeed) {
            newSpeed = playerVelocityRef.Get().OriginalSpeed;
        }

        boostSpeedEvent.Dispatch(new PlayerBoostSpeedEvent.Parameter(newSpeed, playerSpeed.ReturnSpeed));
    }
}
