using IoCPlus;
using UnityEngine;

public class GrapplingHookSetDistanceCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    protected override void Execute() {
        float distance = Vector2.Distance(hookRef.Get().Owner.transform.position, hookRef.Get().HookProjectileGameObject.transform.position);
        grapplingHookRef.Get().HookDistance = distance;
    }
}
