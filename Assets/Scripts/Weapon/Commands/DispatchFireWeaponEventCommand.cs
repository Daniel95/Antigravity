using IoCPlus;
using UnityEngine;

public class DispatchFireWeaponEventCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private FireWeaponEvent fireWeaponEvent;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        fireWeaponEvent.Dispatch(new FireWeaponEvent.Parameter(
            direction,
            weaponRef.Get().SpawnPosition)
        );
    }
}
