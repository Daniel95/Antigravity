using IoCPlus;
using UnityEngine;

public class InstantiateViewInCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer canvasLayer) {
        View prefab = Resources.Load<View>(prefabPath);
        View view = context.InstantiateView(prefab);
        canvasUIRef.Get().AddViewToCanvasLayer(view, canvasLayer);
    }
}
