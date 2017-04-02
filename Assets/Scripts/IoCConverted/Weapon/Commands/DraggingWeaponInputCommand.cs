using IoCPlus;
using UnityEngine;

public class DraggingWeaponInputCommand : Command<Vector2> {

    [Inject] private Ref<IWeaponInput> weaponInputRef;

    protected override void Execute(Vector2 direction) {
        weaponInputRef.Get().Dragging(direction);
    }

}