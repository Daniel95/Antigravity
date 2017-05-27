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
            .GotoState<InActiveContext>()
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.Canceling,
                HookState.HoldingShot
            })
            .Do<SetHookStateCommand>(HookState.Canceling)
            .Do<DispatchHookPullBackEventCommand>();

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
            .Do<DebugLogMessageCommand>("HoldShotEvent")
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.Canceling,
                HookState.HoldingShot })
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
            .Do<DebugLogMessageCommand>("Pullbackhookevent")
            .Do<HookProjectileResetCollidingTransformCommand>()
            .Do<HookProjectileResetParentCommand>()
            .Do<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<HookProjectileMoveTowardsShootDestinationCompletedEvent>()
            .Do<AbortIfCollidingLayerIsNotLayerCommand>(HookableLayer.GrappleSurface)
            .Do<DebugLogMessageCommand>("Attached to GrappleSurface")
            .GotoState<GrapplingHookContext>();

        On<HookProjectileMoveTowardsShootDestinationCompletedEvent>()
            .Do<AbortIfCollidingLayerIsNotLayerCommand>(HookableLayer.PullSurface)
            .Do<DebugLogMessageCommand>("Attached to PullSurface")
            .GotoState<PullingHookContext>();

        On<HookProjectileMoveTowardsShootDestinationCompletedEvent>()
            .Do<AbortIfHookProjectileCollidingLayerIsAHookableLayerCommand>()
            .Do<DebugLogMessageCommand>("No surface to attach")
            .Dispatch<HookProjectileMoveTowardsNextAnchorEvent>();

        On<HookProjectileMoveTowardsNextAnchorEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsNextAnchorEvent")
            .Do<AbortIfHookAnchorCountIsLowerThenOneCommand>()
            .Do<HookProjectileMoveTowardNextAnchorCommand>()
            .OnAbort<DispatchHookProjectileMoveTowardsOwnerEventCommand>();

        On<HookProjectileMoveTowardsNextAnchorCompletedEvent>()
            .Do<DestroyLastHookAnchorCommand>()
            .Dispatch<HookProjectileMoveTowardsNextAnchorEvent>();

        //can be removed
        On<HookProjectileMoveTowardsOwnerEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsOwnerEvent")
            .Do<HookProjectileMoveTowardsOwnerCommand>();

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileReturnedToOwnerEvent")
            .Do<DeactivateHookRopeCommand>()
            .Do<DestroyHookAnchorsCommand>()
            .Do<DeactivateHookProjectileCommand>()
            .Do<AbortIfHookStateIsActive>(HookState.HoldingShot)
            .Do<SetHookStateCommand>(HookState.Inactive)
            .GotoState<InActiveContext>()
            .OnAbort<DispatchShootHookEventCommand>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileSetCollidingTransformToCollider2DTranformCommand>();

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileResetCollidingTransformCommand>();

        On<AddHookAnchorEvent>()
            .Do<AddHookAnchorCommand>();
    }
}



