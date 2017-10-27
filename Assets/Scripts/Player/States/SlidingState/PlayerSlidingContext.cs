using IoCPlus;

public class PlayerSlidingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerState.Sliding)
            .Do<PlayerSetDirectionalMovementCommand>(true);

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<CancelDragInputEvent>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.ConvexCorner)
            .Do<DebugLogMessageCommand>("___________________")
            .Do<DebugLogMessageCommand>("Start Checking Rotate")
            .Do<PlayerStartCheckingRotateAroundCornerConditionsCommand>();
    }
}