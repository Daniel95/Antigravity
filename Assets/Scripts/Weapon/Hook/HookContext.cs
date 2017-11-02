using IoCPlus;
using System.Collections.Generic;

public class HookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabCommand>("Characters/Projectiles/HookProjectile");

        On<LeaveContextSignal>()
            .Do<HookProjectileStopMoveTowardsCommand>()
            .Do<DestroyHookAnchorsCommand>()
            .Do<DeactivateHookProjectileCommand>()
            .Do<StopUpdatePlayerGrapplingCommand>()
            .Do<HookProjectileDestroyCommand>();

        On<LeaveContextSignal>()
            .Do<AbortIfPlayerIsNullCommand>()
            .Do<DeactivateHookCommand>();

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
            .Do<PlayerStopAimLineCommand>();

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
            .Do<HookProjectileClearCollidingGameObjectsCommand>()
            .Do<HookProjectileMoveToShootDirectionCommand>();

        On<PullBackHookEvent>()
            .Do<HookProjectileClearCollidingGameObjectsCommand>()
            .Do<HookProjectileResetParentCommand>()
            .Do<HookProjectileDisableColliderCommand>()
            .GotoState<InActiveContext>()
            .Do<DispatchHookProjectileMoveTowardsNextAnchorCommand>();

        On<HookProjectileMoveTowardsNextAnchorEvent>()
            .Do<AbortIfHookAnchorCountIsLowerOrEqualThenOneCommand>()
            .Do<HookProjectileMoveTowardsNextAnchorCommand>()
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
            .Do<HookProjectileSetParentToFirstCollidingGameObjectsCommand>()
            .Do<HookProjectileStopMoveTowardsCommand>();

        On<HookProjectileTriggerEnterNonPlayerCollision2DEvent>()
            .Do<WaitFramesCommand>(1)
            .Do<AbortIfHookProjectileCollidingLayersDoesNotContainAHookableLayerCommand>()
            .Dispatch<HookProjectileAttachedEvent>()
            .OnAbort<DispatchPullBackHookEventCommand>();

        On<HookProjectileAttachedEvent>()
            .GotoState<GrapplingHookContext>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileAddCollider2DGameObjectToCollidingGameObjectsCommand>();

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<HookProjectileRemoveCollider2DGameObjectFromCollidingGameObjectsCommand>();

        On<AddHookAnchorEvent>()
            .Do<AddHookAnchorCommand>();

        On<UpdateHookEvent>()
            .Do<PlayerPointToClosestAnchorCommand>();
    }
}