using IoCPlus;
using UnityEngine;

public class DraggingWeaponInputCommand : Command {

    [Inject] private Ref<IWeaponInput> weaponInputRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        weaponInputRef.Get().Dragging(direction);
    }

}