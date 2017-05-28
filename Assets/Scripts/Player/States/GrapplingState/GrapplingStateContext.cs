using IoCPlus;

public class GrapplingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<CharacterEnableDirectionalMovementCommand>(false)
            .Do<StartUpdateGrapplingStateCommand>();

        On<LeaveContextSignal>()
            .Do<DebugLogMessageCommand>("LeaveContextSignal GrapplingStateContext")
            .Do<StopUpdateGrapplingStateCommand>()
            .Dispatch<CancelHookEvent>();

        On<JumpInputEvent>()
            .Do<CharacterSetMoveDirectionToVelocityDirectionCommand>()
            .Do<CharacterTemporarySpeedIncreaseCommand>();

        On<NotMovingEvent>()
            .Do<DispatchCharacterTurnToNextDirectionEventCommand>();

        On<UpdateGrapplingStateEvent>()
            .Do<AbortIfNotMovingCommand>()
            .Do<UpdateGrapplingStateVelocityCommand>()
            .Do<CharacterPointToShootDestinationCommand>()
            .OnAbort<DispatchNotMovingEventCommand>();
    }
}