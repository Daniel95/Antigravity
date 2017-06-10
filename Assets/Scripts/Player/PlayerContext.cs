﻿using IoCPlus;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<InputModel>();

        Bind<Ref<IWeapon>>();
        Bind<Ref<IHook>>();
        Bind<Ref<IHookProjectile>>();
        Bind<Ref<IPCInput>>();
        Bind<Ref<IMobileInput>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<ICharacterVelocity>>();
        Bind<Ref<ICharacterTurnDirection>>();
        Bind<Ref<ICharacterRaycastDirection>>();
        Bind<Ref<ICharacterCollisionDirection>>();
        Bind<Ref<ICharacterSurroundingDirection>>();
        Bind<Ref<ICharacterJump>>();
        Bind<Ref<ICharacterDirectionPointer>>();
        Bind<Ref<ICollisionHitDetection>>();
        Bind<Ref<ITriggerHitDetection>>();
        Bind<Ref<ICharacterAimLine>>();
        Bind<Ref<ICharacterBounce>>();
        Bind<Ref<ICharacterDie>>();
        Bind<Ref<ICharacterSpeed>>();
        Bind<Ref<ISlowTime>>();
        Bind<Ref<IGrapplingState>>();
        Bind<Ref<IRevivedState>>();
        BindLabeled<Ref<IMoveTowards>>(Label.Player);

        Bind<CharacterTurnToNextDirectionEvent>();
        Bind<CharacterSetMoveDirectionEvent>();
        Bind<CharacterBoostSpeedEvent>();
        Bind<CharacterTemporarySpeedChangeEvent>();
        Bind<CharacterTemporarySpeedDecreaseEvent>();
        Bind<CharacterTemporarySpeedChangeEvent>();
        Bind<CharacterRemoveCollisionDirectionEvent>();
        Bind<CharacterJumpEvent>();
        Bind<CharacterRetryJumpEvent>();
        Bind<CharacterBounceEvent>();
        Bind<CharacterPointToDirectionEvent>();
        Bind<CharacterSetVelocityEvent>();

        Bind<CancelDragInputEvent>();
        Bind<DraggingInputEvent>();
        Bind<HoldingInputEvent>();
        Bind<JumpInputEvent>();
        Bind<ReleaseInputEvent>();
        Bind<ReleaseInDirectionInputEvent>();
        Bind<TappedExpiredInputEvent>();
        Bind<EnableActionInputEvent>();
        Bind<EnableShootingInputEvent>();

        Bind<PlayerCollisionEnter2DEvent>();
        Bind<PlayerCollisionStay2DEvent>();
        Bind<PlayerCollisionExit2DEvent>();
        Bind<PlayerTriggerEnter2DEvent>();
        Bind<PlayerTriggerStay2DEvent>();
        Bind<PlayerTriggerExit2DEvent>();

        Bind<UpdateGrapplingStateEvent>();

        Bind<RawCancelDragInputEvent>();
        Bind<RawDraggingInputEvent>();
        Bind<RawHoldingInputEvent>();
        Bind<RawJumpInputEvent>();
        Bind<RawReleaseInDirectionInputEvent>();
        Bind<RawReleaseInputEvent>();
        Bind<RawTappedExpiredInputEvent>();

        Bind<FireWeaponEvent>();
        Bind<AimWeaponEvent>();
        Bind<CancelAimWeaponEvent>();

        Bind<UpdateHookEvent>();
        Bind<CancelHookEvent>();
        Bind<HookProjectileMoveTowardsOwnerCompletedEvent>();
        Bind<HookProjectileMoveTowardsNextAnchorEvent>();
        Bind<HoldShotEvent>();

        Bind<EnterGrapplingHookContextEvent>();
        Bind<UpdateGrapplingHookRopeEvent>();

        Bind<EnterPullingHookContextSignal>();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .AddContext<InputContext>()
            .AddContext<WeaponContext>()
            .AddContext<PlayerStateContext>()
            .AddContext<CharacterContext>()
            .Do<DispatchEnableActionInputEventCommand>(true)
            .Do<DispatchEnableShootingInputEventCommand>(true);

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>("Checkpoint")
            .Do<SetCheckpointReachedCommand>(true);

        On<CharacterDieEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<ChooseAndDispatchPlayerDiesEventCommand>();

        On<RespawnPlayerEvent>()
            .Do<InstantiatePlayerCommand>()
            .Do<StartScreenShakeCommand>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionEnter2DEvent>();

        On<CollisionStay2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionStay2DEvent>();

        On<CollisionExit2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionExit2DEvent>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerEnter2DEvent>();

        On<TriggerStay2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerStay2DEvent>();

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerExit2DEvent>();

        On<EnableActionInputEvent>()
            .Do<EnableActionInputCommand>();

        On<EnableShootingInputEvent>()
            .Do<EnableShootingInputCommand>();
    }
}