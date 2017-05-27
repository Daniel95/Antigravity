using IoCPlus;
using UnityEngine;

public class AbortIfHookProjectileCollidingLayerIsAHookableLayerCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        if(hookRef.Get().HookableLayers.Contains(hookProjectileRef.Get().CollidingTransformLayer)) {
            Abort();
        }
    }
}
