using IoCPlus;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        //BindLabeled<Ref<IShoot>>("Views/Player/ShootView");

        Bind<Ref<IShoot>>();
        Bind<Ref<IPCInput>>();
        Bind<Ref<IMobileInput>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<ICharacterVelocity>>();
        Bind<Ref<ICharacterTurnDirection>>();
        Bind<Ref<ICharacterRaycast>>();
        Bind<Ref<ICharacterCollisionDirection>>();
        Bind<Ref<ICharacterJump>>();
        Bind<Ref<ICharacterDirectionPointer>>();
        Bind<Ref<ICharacterCollisionHitDetection>>();
        Bind<Ref<ICharacterTriggerHitDetection>>();
        Bind<Ref<ICharacterAimLine>>();
        Bind<Ref<ICharacterBounce>>();
        Bind<Ref<ICharacterDie>>();
        Bind<Ref<ICharacterSpeed>>();
        Bind<Ref<ISlowTime>>();
        Bind<Ref<IGrapplingState>>();
        Bind<Ref<IRevivedState>>();
        Bind<Ref<IMoveTowards>>();
        Bind<Ref<IHook>>();

        //character
        Bind<CharacterEnableDirectionalMovementEvent>();
        Bind<CharacterSetMoveDirectionEvent>();
        Bind<CharacterBoostSpeedEvent>();
        Bind<CharacterTemporarySpeedChangeEvent>();
        Bind<CharacterTemporarySpeedDecreaseEvent>();
        Bind<CharacterTemporarySpeedChangeEvent>();
        Bind<CharacterRemoveCollisionDirectionEvent>();
        Bind<CharacterJumpEvent>();
        Bind<CharacterRetryJumpEvent>();

        //player
        Bind<CancelDragInputEvent>();
        Bind<DraggingInputEvent>();
        Bind<HoldingInputEvent>();
        Bind<JumpInputEvent>();
        Bind<ReleaseInputEvent>();
        Bind<ReleaseInDirectionInputEvent>();
        Bind<TappedExpiredInputEvent>();
        Bind<EnableActionInputEvent>();
        Bind<EnableShootingInputEvent>();

        //player states
        Bind<UpdateGrapplingStateEvent>();

        //raw inputs
        Bind<RawCancelDragInputEvent>();
        Bind<RawDraggingInputEvent>();
        Bind<RawHoldingInputEvent>();
        Bind<RawJumpInputEvent>();
        Bind<RawReleaseInDirectionInputEvent>();
        Bind<RawReleaseInputEvent>();
        Bind<RawTappedExpiredInputEvent>();

        //weapons
        Bind<SelectedWeaponOutputModel>();
        Bind<FireWeaponEvent>();
        Bind<AimWeaponEvent>();
        Bind<CancelAimWeaponEvent>();

        //hook
        Bind<GrapplingHookStartedEvent>();
        Bind<CancelHookEvent>();
        Bind<HookProjectileReturnedToOwnerEvent>();

        //grapplinghook
        Bind<UpdateGrapplingHookRopeEvent>();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .AddContext<InputContext>()
            .AddContext<WeaponContext>()
            .AddContext<PlayerStateContext>()
            .AddContext<CharacterContext>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>("Checkpoint")
            .Do<SetCheckpointReachedCommand>(true);

        On<CharacterDieEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<ChooseAndDispatchPlayerDiesEventCommand>();

        On<RespawnPlayerEvent>()
            .Do<InstantiatePlayerCommand>()
            .Do<StartScreenShakeCommand>()
            .Dispatch<ActivateRevivedStateEvent>();

        //collisions
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
    }
}