using IoCPlus;

public class WeaponContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<SelectedWeaponOutputModel>();

        Bind<Ref<IWeaponInput>>();
        Bind<Ref<IWeaponOutput>>();

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
            .Do<FireWeaponOutputCommand>();
        On<AimWeaponEvent>()
            .Do<AimWeaponOutputCommand>();
        On<CancelAimWeaponEvent>()
            .Do<CancelAimWeaponOutputCommand>();

    }

}