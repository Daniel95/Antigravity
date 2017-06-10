using IoCPlus;
using UnityEngine;

public class DestroyViewFromCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string viewName, CanvasLayer canvasLayer) {
        View view = canvasUIRef.Get().GetCanvasLayerView(viewName, canvasLayer);
        canvasUIRef.Get().DestroyCanvasLayerView(view, canvasLayer);
    }
}

