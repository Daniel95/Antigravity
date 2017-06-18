using IoCPlus;
using UnityEngine;

public class PlayerUpdateAimLineDestinationCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject(Label.Player)] private Ref<ICharacterAimLine> playerAimLineRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        Vector2 destination = weaponRef.Get().GetShootDestinationPoint(direction);

        playerAimLineRef.Get().UpdateAimLineDestination(destination);
    }
}
