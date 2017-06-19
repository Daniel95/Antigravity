using IoCPlus;
using UnityEngine;

public class PlayerPointToShootDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;
    [Inject] private Ref<IWeapon> weaponRef;

    protected override void Execute() {
        playerDirectionPointerRef.Get().PointToDirection(weaponRef.Get().ShootDirection);
    }
}
