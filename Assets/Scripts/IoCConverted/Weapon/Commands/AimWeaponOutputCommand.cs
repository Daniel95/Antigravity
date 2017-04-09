using IoCPlus;

public class AimWeaponOutputCommand : Command {

    [Inject] private SelectedWeaponOutputModel selectedWeaponOutputModel;

    [Inject] private Ref<IWeaponOutput> weaponOutputRef;

    [InjectParameter] private AimWeaponData aimWeaponData;

    protected override void Execute() {
        weaponOutputRef.Get().Aiming(aimWeaponData.destination, aimWeaponData.spawnPosition);
    }

}