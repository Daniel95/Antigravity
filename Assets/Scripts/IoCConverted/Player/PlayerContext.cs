using IoCPlus;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<CancelDragInputEvent>();
        Bind<DraggingInputEvent>();
        Bind<HoldingInputEvent>();
        Bind<JumpInputEvent>();
        Bind<ReleaseInputEvent>();
        Bind<ReleaseInDirectionInputEvent>();
        Bind<TappedExpiredInputEvent>();
        Bind<EnableActionInputEvent>();
        Bind<EnableShootingInputEvent>();

        Bind<RawCancelDragInputEvent>();
        Bind<RawDraggingInputEvent>();
        Bind<RawHoldingInputEvent>();
        Bind<RawJumpInputEvent>();
        Bind<RawReleaseInDirectionInputEvent>();
        Bind<RawReleaseInputEvent>();
        Bind<RawTappedExpiredInputEvent>();

        Bind<Ref<ICharacterVelocity>>();

        //weapons
        Bind<SelectedWeaponOutputModel>();
        Bind<Ref<IShoot>>();
        Bind<FireWeaponEvent>();
        Bind<AimWeaponEvent>();
        Bind<CancelAimWeaponEvent>();

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
            .Do<InstantiateGameObjectCommand>()
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