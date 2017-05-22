using IoCPlus;

public class GrapplingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<HookProjectileIsAttachedEvent>()
            .Do<GrapplingHookSetDistanceCommand>()
            .Do<AbortIfHookDistanceIsLowerThenMinimalDistance>()
            .Dispatch<EnterGrapplingHookContextEvent>()
            .Do<ChangeSpeedByAngleCommand>()
            .Do<GrapplingHookStartGrappleLockCommand>()
            .OnAbort<DispatchCancelHookEventCommand>();

        On<UpdateGrapplingHookRopeEvent>()
            .Do<UpdateGrapplingHookRopeCommand>();

        On<LeaveContextSignal>()
            .Do<GrapplingHookStopGrappleLockCommand>();

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<GrapplingHookStopGrappleLockCommand>();

    }
}