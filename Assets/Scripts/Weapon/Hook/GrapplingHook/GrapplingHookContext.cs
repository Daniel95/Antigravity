using IoCPlus;

public class GrapplingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<GrapplingHookSetDistanceCommand>()
            .Do<AbortIfHookDistanceIsLowerThenMinimalDistance>()
            .Do<DebugLogMessageCommand>("Start GrapplingHookStartGrappleLockCommand")
            .Dispatch<EnterGrapplingHookContextEvent>()
            .Do<HookProjectileSetParentToCollidingTransformCommand>()
            .Do<ChangeSpeedByAngleCommand>()
            .Do<GrapplingHookStartGrappleLockCommand>()
            .OnAbort<DispatchCancelHookEventCommand>();

        On<UpdateGrapplingHookRopeEvent>()
            .Do<UpdateGrapplingHookRopeCommand>();

        On<LeaveContextSignal>()
            .Do<DebugLogMessageCommand>("LeaveContextSignal GrapplingHookContext")
            .Do<GrapplingHookStopGrappleLockCommand>();
    }
}