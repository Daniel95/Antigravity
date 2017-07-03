using IoCPlus;

public class PlayerSlidingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Sliding)
            .Do<PlayerSetDirectionalMovementCommand>(true);

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<CancelDragInputEvent>()
            .Do<PlayerPointToSavedDirectionCommand>();
    }
}