using IoCPlus;

public class GrapplingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<Ref<IGrapplingState>>();

        On<EnterContextSignal>()
            .Do<InstantiateViewOnPlayerCommand<GrapplingStateView>>()
            .Do<DispatchCharacterEnableDirectionalMovementEventCommand>(false);

        On<LeaveContextSignal>()
            .Dispatch<CancelGrapplingHookEvent>();

        On<JumpInputEvent>()
            .Dispatch<StopGrapplingInAirEvent>();

        On<CancelGrapplingHookEvent>()
            .Dispatch<StopGrapplingInAirEvent>();

        On<StopGrapplingInAirEvent>()
            .Do<SetMoveDirectionToVelocityDirectionCommand>()
            .Do<CharacterTemporarySpeedIncreaseCommand>()
            .Do<StopGrapplingInAirCommand>()
            .Dispatch<ActivateFloatingStateEvent>();
    }
}