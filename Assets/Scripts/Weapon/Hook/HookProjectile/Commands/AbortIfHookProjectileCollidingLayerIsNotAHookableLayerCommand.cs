using IoCPlus;
using UnityEngine;

public class AbortIfHookProjectileCollidingLayerIsNotAHookableLayerCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        if(!hookRef.Get().HookableLayersLayerMask.Contains(hookProjectileRef.Get().CollidingTransformLayer)) {
            Abort();
        }

    }

}
