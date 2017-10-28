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

        On<PlayerTriggerStay2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.ConvexCorner)
            .Do<PlayerStartCheckingRotateAroundCornerConditionsCommand>();

        On<PlayerTriggerExit2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.ConvexCorner)
            .Do<PlayerStopCheckingRotateAroundCornerConditionsCommand>();

    }
}