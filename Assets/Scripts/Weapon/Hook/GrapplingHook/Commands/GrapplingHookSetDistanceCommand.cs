using IoCPlus;
using UnityEngine;

public class GrapplingHookSetDistanceCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;
    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    protected override void Execute() {
        float distance = Vector2.Distance(hookRef.Get().Owner.transform.position, hookProjectileRef.Get().Transform.position);
        grapplingHookRef.Get().HookDistance = distance;
    }
}
