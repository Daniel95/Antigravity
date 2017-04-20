using IoCPlus;
using UnityEngine;

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
            .Do<AbortIfTriggerTagIsNotTheSame>("Checkpoint")
            .Do<SetCheckpointReachedCommand>(true);

        On<CharacterDieEvent>()
            .Do<AbortIfGameObjectIsNotThePlayerCommand>()
            .Do<ChooseAndDispatchPlayerDiesEventCommand>();

        On<RespawnPlayerEvent>()
            .Do<InstantiateGameObjectCommand>()
            .Dispatch<ActivateRevivedStateEvent>();
    }
}