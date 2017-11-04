using IoCPlus;

public class PlayerFloatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerState.Floating)
            .Do<PlayerEnableDirectionalMovementCommand>(true);

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<AbortIfLastHookStateIsNotHookState>(HookState.Shooting)
            .Do<PlayerPointToSavedDirectionCommand>();

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<AbortIfLastHookStateIsHookState>(HookState.Shooting)
            .Do<PlayerPointToVelocityDirectionCommand>();

        On<CancelDragInputEvent>()
            .Do<PlayerPointToVelocityDirectionCommand>();
    }
}