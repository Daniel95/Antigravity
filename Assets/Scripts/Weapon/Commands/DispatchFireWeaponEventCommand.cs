using IoCPlus;
using UnityEngine;

public class DispatchFireWeaponEventCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private FireWeaponEvent fireWeaponEvent;

    protected override void Execute() {
        fireWeaponEvent.Dispatch();
    }
}
