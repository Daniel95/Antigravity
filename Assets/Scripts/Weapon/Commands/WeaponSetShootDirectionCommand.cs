using IoCPlus;
using UnityEngine;

public class WeaponSetShootDirectionCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        weaponRef.Get().ShootDirection = direction;
    }
}
