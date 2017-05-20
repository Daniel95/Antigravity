using UnityEngine;
using System.Collections;
using IoCPlus;

public class HookProjectileSetHookedLayerIndexCommand : Command<int> {

    [Inject] private Ref<IHookProjectile> hookProjectileView;

    protected override void Execute(int layerIndex) {
        hookProjectileView.Get().HookedLayerIndex = layerIndex;
    }
}
