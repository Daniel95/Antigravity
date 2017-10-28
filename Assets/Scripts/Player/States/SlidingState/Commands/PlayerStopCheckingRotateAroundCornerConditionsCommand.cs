using IoCPlus;

public class PlayerStopCheckingRotateAroundCornerConditionsCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterSliding> playerSlidingRef;

    protected override void Execute() {
        playerSlidingRef.Get().StopCheckingRotateAroundCornerConditions();
    }

}
