using IoCPlus;
using UnityEngine;

public class WaitForDestroyChildInCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void ExecuteOverTime(string prefabPath, CanvasLayer canvasLayer) {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab == null) {
            Debug.LogWarning("Can't instantiate view prefab as no prefab is found at given path '" + prefabPath + "'.", prefab);
            return;
        }

        GameObject child = canvasUIRef.Get().GetCanvasLayerChild(canvasLayer, prefabPath);
        canvasUIRef.Get().DestroyCanvasLayerChild(child, canvasLayer, prefabPath, Release);
    }
}

