using IoCPlus;
using UnityEngine;

public class AbortIfHookProjectileCollidingLayersCountIsZeroCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        if(hookProjectileRef.Get().CollidingLayers.Count == 0) {
            Abort();
        }
    }

}
