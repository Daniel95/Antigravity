using IoCPlus;

public class GrapplingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<GrapplingHookSetDistanceCommand>()
            .Do<AbortIfHookDistanceIsLowerThenMinimalDistance>()
            .Do<DebugLogMessageCommand>("Start GrapplingHookStartGrappleLockCommand")
            .Do<GrapplingHookStartGrappleLockCommand>()
            .Do<HookProjectileSetParentToCollidingTransformCommand>()
            .Do<ChangeSpeedByAngleCommand>()
            .Dispatch<EnterGrapplingHookContextEvent>()
            .OnAbort<DispatchCancelHookEventCommand>();

        On<UpdateGrapplingHookRopeEvent>()
            .Do<UpdateGrapplingHookRopeCommand>();

        On<LeaveContextSignal>()
            .Do<DebugLogMessageCommand>("LeaveContextSignal GrapplingHookContext")
            .Do<GrapplingHookStopGrappleLockCommand>();
    }
}