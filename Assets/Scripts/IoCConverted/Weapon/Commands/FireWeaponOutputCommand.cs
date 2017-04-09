using IoCPlus;
using UnityEngine;

public class FireWeaponOutputCommand : Command {

    [Inject] private SelectedWeaponOutputModel selectedWeaponOutputModel;

    [Inject] private Ref<IWeaponOutput> weaponOutputRef;

    [InjectParameter] private AimWeaponData aimWeaponData;

    protected override void Execute() {
        weaponOutputRef.Get().Fire(aimWeaponData.destination, aimWeaponData.spawnPosition);
    }

}