using IoCPlus;

public class SlidingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Sliding)
            .Do<PlayerEnableDirectionalMovementCommand>(true);

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<CancelDragInputEvent>()
            .Do<PlayerPointToSavedDirectionCommand>();
    }
}