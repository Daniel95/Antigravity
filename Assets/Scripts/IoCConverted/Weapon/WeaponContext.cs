using IoCPlus;
using UnityEngine;

public class WeaponContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<SelectedWeaponOutputModel>();
        Bind<HookModel>();

        Bind<Ref<IWeaponInput>>();
        Bind<Ref<IWeaponOutput>>();
        Bind<Ref<IPullingHook>>();
        Bind<Ref<IGrapplingHook>>();

        Bind<GrapplingHookStartedEvent>();
        Bind<ChangeSpeedByAngleEvent>();
        Bind<AddAnchorEvent>();
        Bind<CancelHookEvent>();

        Bind<FireWeaponEvent>();
        Bind<AimWeaponEvent>();
        Bind<CancelAimWeaponEvent>();

        On<EnterContextSignal>()
            .Do<ActivateViewOnPlayerCommand<WeaponInputView>>();

        On<DraggingInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<DraggingWeaponInputCommand>();
        On<CancelDragInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<CancelDragWeaponInputCommand>();
        On<ReleaseInDirectionInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<ReleaseInDirectionWeaponInputCommand>();
        On<FireWeaponEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<FireWeaponOutputCommand>();
        On<AimWeaponEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<AimWeaponOutputCommand>();
        On<CancelAimWeaponEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<CancelAimWeaponOutputCommand>();
    }

}