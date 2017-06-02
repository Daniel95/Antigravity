using IoCPlus;

public class SlidingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Sliding)
            .Do<CharacterActivateDirectionalMovementCommand>();

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<CharacterPointToSavedDirectionCommand>();

        On<CancelDragInputEvent>()
            .Do<CharacterPointToSavedDirectionCommand>();
    }
}