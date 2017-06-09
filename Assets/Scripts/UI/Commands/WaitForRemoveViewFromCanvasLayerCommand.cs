using IoCPlus;
using UnityEngine;

public class WaitForRemoveViewFromCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void ExecuteOverTime(string viewName, CanvasLayer canvasLayer) {
        View view = canvasUIRef.Get().GetCanvasLayerContentView(viewName, canvasLayer);

        DestroyUIView destroyUIView = view.GetComponent<DestroyUIView>();

        if (destroyUIView == null) {
            Debug.LogWarning("Child " + viewName + " in " + canvasLayer.ToString() + "doesn't have PopOutUIView component.");
            return;
        }

        destroyUIView.PopOutAndDestroy(Release);
    }
}

