using IoCPlus;
using UnityEngine;

public class ReleaseInDirectionWeaponInputCommand : Command<Vector2> {

    [Inject] private Ref<IWeaponInput> weaponInputRef;

    protected override void Execute(Vector2 direction) {
        weaponInputRef.Get().ReleaseInDirection(direction);
    }

}