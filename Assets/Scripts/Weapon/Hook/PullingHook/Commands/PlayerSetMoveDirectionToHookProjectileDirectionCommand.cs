using IoCPlus;
using UnityEngine;

public class PlayerSetMoveDirectionToHookProjectileDirectionCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        Vector2 newDirection = (hookProjectileRef.Get().Transform.position - weaponRef.Get().Owner.transform.position).normalized;
        playerVelocityRef.Get().SetMoveDirection(newDirection);
    }
}
