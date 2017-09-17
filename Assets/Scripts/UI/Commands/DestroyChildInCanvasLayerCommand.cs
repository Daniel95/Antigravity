using IoCPlus;
using UnityEngine;

public class DestroyChildInCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer canvasLayer) {
        GameObject child = canvasUIRef.Get().GetCanvasLayerChild(canvasLayer, prefabPath);
        canvasUIRef.Get().DestroyCanvasLayerChild(child, canvasLayer, prefabPath);
    }
}

