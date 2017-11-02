using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfHookProjectileCollidingLayersDoesNotContainAHookableLayerCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        List<int> collidingLayers = hookProjectileRef.Get().CollidingLayers;
        LayerMask hookableLayersLayerMask = hookRef.Get().HookableLayersLayerMask;
        bool collidingLayersContainsHookableLayer = collidingLayers.Exists(x => hookableLayersLayerMask.Contains(x));
        if (!collidingLayersContainsHookableLayer) {
            Abort();
        }
    }

}
