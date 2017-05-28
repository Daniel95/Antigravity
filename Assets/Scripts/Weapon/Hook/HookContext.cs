using IoCPlus;
using System.Collections.Generic;

public class HookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        BindLabeled<Ref<IMoveTowards>>(Label.HookProjectile);

        On<EnterContextSignal>()
            .Do<InstantiateHookProjectileCommand>();

        On<LeaveContextSignal>()
            .Do<HookProjectileDestroyCommand>();

        On<CancelHookEvent>()
            .Do<StopSlowTimeCommand>()
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.Canceling,
                HookState.HoldingShot
            })
            .Do<SetHookStateCommand>(HookState.Canceling)
            .Dispatch<PullBackHookEvent>();

        On<AimWeaponEvent>()
            .Do<CharacterUpdateAimLineDestinationCommand>();

        On<CancelAimWeaponEvent>()
            .Do<StopSlowTimeCommand>()
            .Do<CharacterStopAimLineCommand>();

        On<FireWeaponEvent>()
            .Do<DebugLogMessageCommand>("FireWeaponEvent")
            .Do<SetHookDestinationCommand>()
            .Do<AbortIfHookStateIsNotActive>(HookState.Inactive)
            .Do<DispatchShootHookEventCommand>()
            .OnAbort<DispatchHoldShotEventCommand>();

        On<HoldShotEvent>()
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.HoldingShot })
            .Do<DebugLogMessageCommand>("HoldShotEvent")
            .Do<SetHookStateCommand>(HookState.HoldingShot)
            .Do<DispatchHookPullBackEventCommand>();

        On<ShootHookEvent>()
            .Do<DebugLogMessageCommand>("ShootHookEvent")
            .Do<SetHookStateCommand>(HookState.Shooting)
            .Do<ActivateHookProjectileCommand>()
            .Do<SpawnHookProjectileAnchorCommand>()
            .Do<ActivateHoopRopeCommand>()
            .Do<HookProjectileResetCollidingTransformCommand>(0)
            .Do<HookProjectileGoToShootDestinationCommand>();

        On<PullBackHookEvent>()
            .Do<DebugLogMessageCommand>("PullBackHookEvent")
            .Do<HookProjectileResetCollidingTransformCommand>()
            .Do<HookProjectileResetParentCommand>()
            .GotoState<InActiveContext>()
            .Do<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<HookProjectileMoveTowardsShootDestinationCompletedEvent>()
            .Do<AbortIfCollidingLayerIsNotLayerCommand>(HookableLayer.GrappleSurface)
            .Do<DebugLogMessageCommand>("GotoState<GrapplingHookContext>()")
            .GotoState<GrapplingHookContext>();

        On<HookProjectileMoveTowardsShootDestinationCompletedEvent>()
            .Do<AbortIfCollidingLayerIsNotLayerCommand>(HookableLayer.PullSurface)
            .Do<DebugLogMessageCommand>("GotoState<PullingHookContext>()")
            .GotoState<PullingHookContext>();

        On<HookProjectileMoveTowardsShootDestinationCompletedEvent>()
            .Do<AbortIfHookProjectileCollidingLayerIsAHookableLayerCommand>()
            .Dispatch<HookProjectileMoveTowardsNextAnchorEvent>();

        On<HookProjectileMoveTowardsNextAnchorEvent>()
            .Do<AbortIfHookAnchorCountIsLowerOrEqualThenOneCommand>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsNextAnchorEvent")
            .Do<HookProjectileMoveTowardNextAnchorCommand>()
            .OnAbort<DispatchHookProjectileMoveTowardsOwnerEventCommand>();

        On<HookProjectileMoveTowardsNextAnchorCompletedEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsNextAnchorCompletedEvent")
            .Do<DestroyOneButLastHookAnchorCommand>()
            .Dispatch<HookProjectileMoveTowardsNextAnchorEvent>();

        On<HookProjectileMoveTowardsOwnerEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsOwnerEvent")
            .Do<HookProjectileMoveTowardsOwnerCommand>();

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<DebugClearLogCommand>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsOwnerCompletedEvent")
            .Do<DeactivateHookRopeCommand>()
            .Do<DestroyHookAnchorsCommand>()
            .Do<DeactivateHookProjectileCommand>()
            .GotoState<InActiveContext>()
            .Do<AbortIfHookStateIsActive>(HookState.HoldingShot)
            .Do<SetHookStateCommand>(HookState.Inactive)
            .OnAbort<DispatchShootHookEventCommand>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfColliderIsNotATriggerCommand>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileSetCollidingTransformToCollider2DTranformCommand>();

        On<TriggerExit2DEvent>()
            .Do<AbortIfColliderIsNotATriggerCommand>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileResetCollidingTransformCommand>();

        On<AddHookAnchorEvent>()
            .Do<AddHookAnchorCommand>();
    }
}



