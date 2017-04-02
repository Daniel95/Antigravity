using IoCPlus;
using UnityEngine;

public class FireWeaponOutputCommand : Command<Vector2, Vector2> {

    [Inject] private SelectedWeaponOutputModel selectedWeaponOutputModel;

    [Inject] private Ref<IWeaponOutput> weaponOutputRef;

    protected override void Execute(Vector2 destination, Vector2 spawnPosition) {
        weaponOutputRef.Get().Fire(destination, spawnPosition);
    }

}