using IoCPlus;
using UnityEngine;

public class AbortIfHookProjectileAnchorIndexIsHigherOrEqualThenAnchorCount : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        if (hookProjectileRef.Get().AnchorsIndex >= hookRef.Get().Anchors.Count) {
            Debug.Log("abort");
            hookProjectileRef.Get().AnchorsIndex = 0;
            Abort();
        }
    }
}
