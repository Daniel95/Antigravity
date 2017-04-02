using IoCPlus;

public class CancelAimWeaponOutputCommand : Command {

    [Inject] private SelectedWeaponOutputModel selectedWeaponOutputModel;

    [Inject] private Ref<IWeaponOutput> weaponOutputRef;

    protected override void Execute() {
        weaponOutputRef.Get().CancelAiming();
    }

}