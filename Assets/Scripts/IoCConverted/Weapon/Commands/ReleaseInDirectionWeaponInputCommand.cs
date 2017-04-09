using IoCPlus;
using UnityEngine;

public class ReleaseInDirectionWeaponInputCommand : Command {

    [Inject] private Ref<IWeaponInput> weaponInputRef;
    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        weaponInputRef.Get().ReleaseInDirection(direction);
    }

}