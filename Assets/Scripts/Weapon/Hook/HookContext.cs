using IoCPlus;
using System.Collections.Generic;

public class HookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        BindLabeled<Ref<IMoveTowards>>(Label.HookProjectile);

        On<EnterContextSignal>()
            .Do<HookProjectileInstantiateCommand>();

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

        On<CancelAimWeaponEvent>()
            .Do<CharacterStopAimLineCommand>();

        On<FireWeaponEvent>()
            .Do<AbortIfHookStateIsNotActive>(HookState.Inactive)
            .Do<DispatchShootHookEventCommand>()
            .OnAbort<DispatchHoldShotEventCommand>();

        On<HoldShotEvent>()
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.HoldingShot })
            .Do<SetHookStateCommand>(HookState.HoldingShot)
            .Do<DispatchHookPullBackEventCommand>();

        On<ShootHookEvent>()
            .Do<SetHookStateCommand>(HookState.Shooting)
            .Do<ActivateHookProjectileCommand>()
            .Do<HookProjectileEnableColliderCommand>()
            .Do<SpawnHookProjectileAnchorCommand>()
            .Do<ActivateHookCommand>()
            .Do<HookProjectileResetCollidingTransformCommand>(0)
            .Do<HookProjectileMoveToShootDirectionCommand>();

        On<PullBackHookEvent>()
            .Do<HookProjectileResetCollidingTransformCommand>()
            .Do<HookProjectileResetParentCommand>()
            .Do<HookProjectileDisableColliderCommand>()
            .GotoState<InActiveContext>()
            .Do<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<HookProjectileMoveTowardsNextAnchorEvent>()
            .Do<AbortIfHookAnchorCountIsLowerOrEqualThenOneCommand>()
            .Do<HookProjectileMoveTowardNextAnchorCommand>()
            .OnAbort<HookProjectileMoveTowardsOwnerCommand>();

        On<HookProjectileMoveTowardsNextAnchorCompletedEvent>()
            .Do<DestroyOneButLastHookAnchorCommand>()
            .Dispatch<HookProjectileMoveTowardsNextAnchorEvent>();

        On<HookProjectileMoveTowardsOwnerCompletedEvent>()
            .Do<DeactivateHookCommand>()
            .Do<DestroyHookAnchorsCommand>()
            .Do<DeactivateHookProjectileCommand>()
            .GotoState<InActiveContext>()
            .Do<AbortIfHookStateIsActive>(HookState.HoldingShot)
            .Do<SetHookStateCommand>(HookState.Inactive)
            .OnAbort<DispatchShootHookEventCommand>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfHookStateIsNotActive>(HookState.Shooting)
            .Do<AbortIfColliderIsATriggerCommand>()
            .Do<AbortIfColliderIsPlayerCommand>()
            .Dispatch<HookProjectileTriggerEnterNonPlayerCollision2DEvent>();

        On<HookProjectileTriggerEnterNonPlayerCollision2DEvent>()
            .Do<HookProjectileSetParentToCollidingTransformCommand>()
            .Do<HookProjectileStopMoveTowardsCommand>();

        On<HookProjectileTriggerEnterNonPlayerCollision2DEvent>()
            .Do<WaitFrameCommand>()
            .Do<AbortIfHookProjectileCollidingLayerIsAHookableLayerCommand>()
            .Dispatch<HookProjectileAttachedEvent>()
            .OnAbort<DispatchPullBackHookEventCommand>();

        On<HookProjectileAttachedEvent>()
            .Do<AbortIfCollidingLayerIsNotLayerCommand>(HookableLayer.GrappleSurface)
            .GotoState<GrapplingHookContext>();

        On<HookProjectileAttachedEvent>()
            .Do<AbortIfCollidingLayerIsNotLayerCommand>(HookableLayer.PullSurface)
            .GotoState<PullingHookContext>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfColliderIsNotATriggerCommand>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileSetCollidingTransformToCollider2DTranformCommand>();

        On<TriggerStay2DEvent>()
            .Do<AbortIfColliderIsNotATriggerCommand>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileSetCollidingTransformToCollider2DTranformCommand>();

        On<TriggerExit2DEvent>()
            .Do<AbortIfColliderIsNotATriggerCommand>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileResetCollidingTransformCommand>();

        On<AddHookAnchorEvent>()
            .Do<AddHookAnchorCommand>();

        On<UpdateHookEvent>()
            .Do<CharacterPointToClosestAnchorCommand>();
    }
}