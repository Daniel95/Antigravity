using IoCPlus;
using UnityEngine;

public class AbortIfHookProjectileCollidingLayersDoesNotContainLayerCommand : Command<int> {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute(int layer) {
        if (hookProjectileRef.Get().CollidingLayers.Contains(layer)) {
            Abort();
        }
    }
}
