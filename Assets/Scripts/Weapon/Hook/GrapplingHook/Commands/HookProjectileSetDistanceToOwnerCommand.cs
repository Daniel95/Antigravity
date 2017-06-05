using IoCPlus;
using UnityEngine;

public class HookProjectileSetDistanceToOwnerCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        float distance = Vector2.Distance(weaponRef.Get().Owner.transform.position, hookProjectileRef.Get().Transform.position);
        hookProjectileRef.Get().DistanceFromOwner = distance;
    }
}
