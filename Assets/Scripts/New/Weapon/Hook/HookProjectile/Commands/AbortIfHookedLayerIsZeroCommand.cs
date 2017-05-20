using IoCPlus;
using UnityEngine;

public class AbortIfHookedLayerIsZeroCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        Debug.Log(hookProjectileRef.Get().HookedLayerIndex);
        if(hookProjectileRef.Get().HookedLayerIndex == 0) {
            Abort();
        }
    }
}
