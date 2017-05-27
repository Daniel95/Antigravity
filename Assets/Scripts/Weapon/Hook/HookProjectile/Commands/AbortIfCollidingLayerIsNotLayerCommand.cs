using IoCPlus;
using UnityEngine;

public class AbortIfCollidingLayerIsNotLayerCommand : Command<int> {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute(int layer) {
        if (hookProjectileRef.Get().CollidingTransformLayer != layer) {
            Abort();
        }
    }
}
