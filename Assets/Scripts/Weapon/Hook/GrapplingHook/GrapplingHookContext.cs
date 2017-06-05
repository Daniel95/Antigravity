using IoCPlus;

public class GrapplingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<GrapplingHookSetDistanceCommand>()
            .Do<AbortIfHookDistanceIsLowerThenMinimalDistance>()
            .Do<GrapplingHookStartGrappleLockCommand>()
            .Do<ChangeSpeedByAngleCommand>()
            .Dispatch<EnterGrapplingHookContextEvent>()
            .OnAbort<DispatchCancelHookEventCommand>();

        On<UpdateGrapplingHookRopeEvent>()
            .Do<UpdateGrapplingHookRopeCommand>();

        On<LeaveContextSignal>()
            .Do<GrapplingHookStopGrappleLockCommand>();
    }
}