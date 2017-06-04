using IoCPlus;
using UnityEngine;

public class CharacterUpdateAimLineDestinationCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<ICharacterAimLine> aimLineRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        Vector2 destination = weaponRef.Get().GetShootDestinationPoint(direction);

        aimLineRef.Get().UpdateAimLineDestination(destination);
    }
}
