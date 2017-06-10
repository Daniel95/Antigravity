using IoCPlus;
using UnityEngine;

public class WaitForRemoveViewFromCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void ExecuteOverTime(string viewName, CanvasLayer canvasLayer) {
        View view = canvasUIRef.Get().GetCanvasLayerView(viewName, canvasLayer);
        canvasUIRef.Get().DestroyCanvasLayerView(view, canvasLayer, Release);
    }
}

