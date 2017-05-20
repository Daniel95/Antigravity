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
            .Do<AbortIfHookStateIsNotActive>(HookState.Inactive)
            .Do<SetHookDestinationCommand>()
            .Do<DispatchShootHookEventCommand>()
            .OnAbort<DispatchHoldShotEventCommand>();

        On<HoldShotEvent>()
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.Canceling,
                HookState.HoldingShot })
            .Do<SetHookStateCommand>(HookState.HoldingShot)
            .Do<AbortIfHookStateIsLastHookState>(HookState.Canceling)
            .Do<DispatchHookPullBackEventCommand>();

        On<ShootHookEvent>()
            .Do<SetHookStateCommand>(HookState.Shooting)
            .Do<ActivateHookProjectileCommand>()
            .Do<SpawnAnchorAtPlayerCommand>()
            .Do<ActivateHoopRopeCommand>()
            .Do<SetHookedLayerCommand>(0)
            .Do<HookProjectileGoToShootDestinationCommand>();

        On<PullBackHookEvent>()
            .Do<HookProjectileResetAttachedTransformCommand>()
            .Do<HookProjectileSetHookedLayerIndexCommand>(0)
            .Do<HookProjectileResetParentCommand>()
            .Do<HookProjectileSetReachedAnchorsIndexCommand>(0)
            .Do<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<HookProjectileMoveTowardsShootDestinationCompletedEvent>()
            .Do<AbortIfHookedLayerIsZeroCommand>()
            .Do<HookProjectileSetParentToAttachedTransformCommand>()
            .Do<DispatchHookProjectileIsAttachedEventCommand>()
            .OnAbort<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<HookProjectileMoveTowardsNextAnchorEvent>()
            .Do<AbortIfHookProjectileAnchorIndexIsHigherOrEqualThenAnchorCount>()
            .Do<HookProjectileMoveTowardNextAnchorCommand>()
            .OnAbort<DispatchHookProjectileMoveTowardsOwnerEventCommand>();

        On<HookProjectileMoveTowardsOwnerEvent>()
            .Do<AbortIfHookProjectileIsAlreadyMovingToOwnerCommand>()
            .Do<HookProjectileMoveTowardsOwnerCommand>()
            .OnAbort<DispatchHookProjectileReturnedToOwnerEventCommand>();

        On<HookProjectileReturnedToOwnerEvent>()
            .Do<AbortIfHookStateIsActive>(HookState.HoldingShot)
            .Do<SetHookStateCommand>(HookState.Inactive)
            .Do<DeactivateHookProjectileCommand>()
            .Do<DestroyHookAnchorsCommand>()
            .Do<DeactivateHookRopeCommand>()
            .OnAbort<DispatchShootHookEventCommand>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexIsNotTheSameCommand>(HookableLayers.GrappleSurface)
            .Do<HookProjectileSetAttachedTransformCommand>()
            .Do<HookProjectileSetHookedLayerIndexCommand>(HookableLayers.GrappleSurface);

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexIsNotTheSameCommand>(HookableLayers.PullSurface)
            .Do<HookProjectileSetHookedLayerIndexCommand>(HookableLayers.PullSurface);

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexesAreNotTheSameCommand>(new List<int> { HookableLayers.GrappleSurface, HookableLayers.PullSurface })
            .Do<HookProjectileSetHookedLayerIndexCommand>(0);

        On<AddHookAnchorEvent>()
            .Do<AddHookAnchorCommand>();
    }
}



