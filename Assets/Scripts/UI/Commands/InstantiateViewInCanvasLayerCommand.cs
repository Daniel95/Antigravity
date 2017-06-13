using IoCPlus;
using UnityEngine;

public class InstantiateViewInCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer canvasLayer) {
        View prefab = Resources.Load<View>(prefabPath);
        if (prefab == null) {
            Debug.LogWarning("Can't instantiate view prefab as no prefab is found at given path '" + prefabPath + "'.");
            return;
        }
        View view = context.InstantiateView(prefab);
        canvasUIRef.Get().AddChildToCanvasLayer(view.gameObject, canvasLayer);
    }
}
