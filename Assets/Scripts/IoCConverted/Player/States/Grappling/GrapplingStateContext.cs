using IoCPlus;

public class GrapplingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<DispatchCharacterEnableDirectionalMovementEventCommand>(false);

        On<LeaveContextSignal>()
            .Dispatch<CancelHookEvent>();

        On<JumpInputEvent>()
            .Dispatch<StopGrapplingInAirEvent>();

        On<StopGrapplingInAirEvent>()
            .Do<CharacterSetMoveDirectionToVelocityDirectionCommand>()
            .Do<CharacterTemporarySpeedIncreaseCommand>()
            .Dispatch<ActivateFloatingStateEvent>();

        On<UpdateGrapplingStateEvent>()
            .Do<UpdateGrapplingStateCommand>();
    }
}