using IoCPlus;

public class GrapplingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Grappling)
            .Do<PlayerEnableDirectionalMovementCommand>(false)
            .Do<StartUpdateGrapplingStateCommand>();

        On<LeaveContextSignal>()
            .Do<StopUpdateGrapplingStateCommand>()
            .Dispatch<CancelHookEvent>();

        On<JumpInputEvent>()
            .Do<PlayerSetMoveDirectionToVelocityDirectionCommand>()
            .Do<PlayerTemporarySpeedIncreaseCommand>();

        On<UpdateGrapplingStateEvent>()
            .Do<PlayerSetVelocityToVelocityDirectionSpeedCommand>();

        On<UpdateGrapplingStateEvent>()
            .Do<AbortIfPlayerIsNotStuckCommand>()
            .Do<DispatchPlayerTurnToNextDirectionEventCommand>()
            .Dispatch<PlayerIsStuckEvent>();
    }
}