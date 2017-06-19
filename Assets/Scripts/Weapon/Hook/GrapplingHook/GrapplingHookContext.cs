﻿using IoCPlus;

public class GrapplingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<HookProjectileSetDistanceToOwnerCommand>()
            .Do<HookProjectileAbortIfDistanceToOwnerIsHigherThenMinimalDistance>()
            .Do<GrapplingHookStartGrappleLockCommand>()
            .Do<PlayerChangeSpeedByAngleCommand>()
            .Dispatch<EnterGrapplingHookContextEvent>()
            .OnAbort<DispatchCancelHookEventCommand>();

        On<UpdateGrapplingHookRopeEvent>()
            .Do<UpdateGrapplingHookRopeCommand>();

        On<LeaveContextSignal>()
            .Do<GrapplingHookStopGrappleLockCommand>();
    }
}