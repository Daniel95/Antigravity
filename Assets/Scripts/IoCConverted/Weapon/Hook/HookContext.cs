using IoCPlus;
using System.Collections.Generic;

public class HookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GrapplingHookStartedEvent>();
        Bind<CancelGrapplingHookEvent>();

        Bind<HookProjectileReturnedToOwnerEvent>();

        Bind<Ref<IHook>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<IPullingHook>>();
        Bind<Ref<IHookProjectile>>();

        On<EnterContextSignal>();

        On<CancelGrapplingHookEvent>()
            .Do<StopSlowTimeCommand>()
            .Do<DispatchHookPullBackEventCommand>();

        //StopHoldHookShotCommand


        On<FireWeaponEvent>()
            .Do<AbortIfHookStateIsNotActive>(HookState.Inactive)
            .Do<DispatchShootHookEventCommand>()
            .OnAbort<DispatchHookPullBackEventCommand>();

        //StartHoldHookShotCommand

        On<ShootHookEvent>()
            .Do<SetHookStateCommand>(HookState.BusyShooting)
            .Do<ActivateHookProjectileCommand>()
            .Do<SpawnAnchorAtPlayerCommand>()
            .Do<ActivateHoopRopeCommand>()
            .Do<SetHookedLayerCommand>(0)
            .Do<HookProjectileGoToDestinationCommand>();

        On<PullBackHookEvent>()
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.BusyPullingBack })
            .Do<SetHookStateCommand>(HookState.BusyPullingBack)
            .Do<HookProjectileSetAttachedTransformCommand>(null)
            .Do<HookProjectileSetHookedLayerIndexCommand>(0)
            .Do<HookProjectileResetParentCommand>()
            .Do<HookProjectileSetReachedAnchorsIndexCommand>(0)
            .Do<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<ReachedDestinationEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
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
            .Do<SetHookStateCommand>(HookState.Inactive)
            .Do<DeactivateHookProjectileCommand>()
            .Do<DestroyHookAnchorsCommand>()
            .Do<DeactivateHookRopeCommand>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexIsNotTheSameCommand>(HookAbleLayers.GrappleSurface)
            .Do<HookProjectileSetAttachedTransformCommand>()
            .Do<HookProjectileSetHookedLayerIndexCommand>(HookAbleLayers.GrappleSurface);

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexIsNotTheSameCommand>(HookAbleLayers.PullSurface)
            .Do<HookProjectileSetHookedLayerIndexCommand>(HookAbleLayers.PullSurface);

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexesAreNotTheSameCommand>(new List<int> { HookAbleLayers.GrappleSurface, HookAbleLayers.PullSurface })
            .Do<HookProjectileSetHookedLayerIndexCommand>(0);
    }
}



