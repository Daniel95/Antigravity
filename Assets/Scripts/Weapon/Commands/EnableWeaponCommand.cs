using IoCPlus;

public class EnableWeaponCommand : Command<bool> {

    [Inject] private WeaponStatus weaponStatus;

    protected override void Execute(bool enable) {
        weaponStatus.WeaponIsEnabled = enable;
    }
}