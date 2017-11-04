using IoCPlus;

public class PlayerSlidingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerState.Sliding)
            .Do<PlayerEnableDirectionalMovementCommand>(true);

        On<LeaveContextSignal>()
            .Do<PlayerStopAllCheckingRotateAroundCornerConditionsCommand>();

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<CancelDragInputEvent>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<PlayerTriggerStay2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.ConvexCorner)
            .Do<PlayerStartCheckingRotateAroundCornerTransformConditionsCommand>();

        On<PlayerTriggerExit2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.ConvexCorner)
            .Do<PlayerStopCheckingRotateAroundCornerTransformConditionsCommand>();

    }
}