using IoCPlus;
using UnityEngine;

public class GrapplingHookSetDistanceCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;
    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    protected override void Execute() {
        float distance = Vector2.Distance(weaponRef.Get().Owner.transform.position, hookProjectileRef.Get().Transform.position);
        grapplingHookRef.Get().HookDistance = distance;
    }
}
