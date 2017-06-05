using IoCPlus;
using UnityEngine;

public class CharacterPointToShootDirectionCommand : Command {

    [Inject] private Ref<ICharacterDirectionPointer> directionPointerRef;
    [Inject] private Ref<IWeapon> weaponRef;

    protected override void Execute() {
        directionPointerRef.Get().PointToDirection(weaponRef.Get().ShootDirection);
    }
}
