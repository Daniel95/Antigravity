using IoCPlus;
using UnityEngine;

public class RemoveViewFromCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string viewName, CanvasLayer canvasLayer) {
        View view = canvasUIRef.Get().GetCanvasLayerContentView(viewName, canvasLayer);
        canvasUIRef.Get().RemoveCanvasLayerContentView(view, canvasLayer);

        PopOutAndDestroyUIView destroyUIView = view.GetComponent<PopOutAndDestroyUIView>();

        if (destroyUIView != null) {
            destroyUIView.PopOutAndDestroy();
            return;
        }

        view.Destroy();
    }
}

