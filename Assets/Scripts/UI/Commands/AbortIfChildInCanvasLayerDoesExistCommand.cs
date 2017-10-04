using IoCPlus;

public class AbortIfChildInCanvasLayerDoesExistCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer canvasLayer) {
        if(canvasUIRef.Get().ContainsCanvasLayerChild(canvasLayer, prefabPath)) {
            Abort();
        }
    }
}

