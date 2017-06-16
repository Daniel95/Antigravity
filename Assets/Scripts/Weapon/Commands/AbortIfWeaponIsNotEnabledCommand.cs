using IoCPlus;

public class AbortIfWeaponIsNotEnabledCommand : Command {

    [Inject] private WeaponStatus weaponStatus;

    protected override void Execute() {
        if(!weaponStatus.WeaponIsEnabled) {
            Abort();
        }
    }
}