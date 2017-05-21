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
            .Do<SetHookedLayerCommand>(0)
            .Do<HookProjectileGoToShootDestinationCommand>();

        On<PullBackHookEvent>()
            .Do<DebugLogMessageCommand>("Pullbackhookevent")
            .Do<HookProjectileResetAttachedTransformCommand>()
            .Do<HookProjectileSetHookedLayerIndexCommand>(0)
            .Do<HookProjectileResetParentCommand>()
            .Do<HookProjectileSetReachedAnchorsIndexCommand>(0)
            .Do<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<HookProjectileMoveTowardsShootDestinationCompletedEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsShootDestinationCompletedEvent")
            .Do<AbortIfHookedLayerIsZeroCommand>()
            .Do<HookProjectileSetParentToAttachedTransformCommand>()
            .Do<DispatchHookProjectileIsAttachedEventCommand>()
            .OnAbort<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<HookProjectileMoveTowardsNextAnchorEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsNextAnchorEvent")
            .Do<AbortIfHookProjectileAnchorIndexIsHigherOrEqualThenAnchorCount>()
            .Do<HookProjectileMoveTowardNextAnchorCommand>()
            .OnAbort<DispatchHookProjectileMoveTowardsOwnerEventCommand>();

        On<HookProjectileMoveTowardsOwnerEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileMoveTowardsOwnerEvent")
            .Do<AbortIfHookProjectileIsAlreadyMovingToOwnerCommand>()
            .Do<HookProjectileMoveTowardsOwnerCommand>()
            .OnAbort<DispatchHookProjectileReturnedToOwnerEventCommand>();

        On<HookProjectileReturnedToOwnerEvent>()
            .Do<DebugLogMessageCommand>("HookProjectileReturnedToOwnerEvent")
            .Do<DeactivateHookRopeCommand>()
            .Do<DestroyHookAnchorsCommand>()
            .Do<AbortIfHookStateIsActive>(HookState.HoldingShot)
            .Do<SetHookStateCommand>(HookState.Inactive)
            .Do<DeactivateHookProjectileCommand>()
            .GotoState<InActiveContext>()
            .OnAbort<DispatchShootHookEventCommand>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexIsNotTheSameCommand>(HookableLayers.GrappleSurface)
            .Do<DebugLogMessageCommand>("collided with GrappleSurface")
            .Do<HookProjectileSetAttachedTransformCommand>()
            .GotoState<GrapplingHookContext>()
            .Do<HookProjectileSetHookedLayerIndexCommand>(HookableLayers.GrappleSurface);

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexIsNotTheSameCommand>(HookableLayers.PullSurface)
            .Do<DebugLogMessageCommand>("collided with PullSurface")
            .GotoState<PullingHookContext>()
            .Do<HookProjectileSetHookedLayerIndexCommand>(HookableLayers.PullSurface);

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexesAreNotTheSameCommand>(new List<int> { HookableLayers.GrappleSurface, HookableLayers.PullSurface })
            .Do<HookProjectileSetHookedLayerIndexCommand>(0);

        On<AddHookAnchorEvent>()
            .Do<AddHookAnchorCommand>();
    }
}



