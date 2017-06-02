using IoCPlus;

public class FloatingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Floating)
            .Do<CharacterEnableDirectionalMovementCommand>(true);

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<CharacterPointToVelocityDirectionCommand>();

        On<CancelDragInputEvent>()
            .Do<CharacterPointToVelocityDirectionCommand>();
    }
}