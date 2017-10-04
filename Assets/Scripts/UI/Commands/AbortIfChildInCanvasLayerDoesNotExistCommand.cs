using IoCPlus;

public class AbortIfChildInCanvasLayerDoesNotExistCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer canvasLayer) {
        if(!canvasUIRef.Get().ContainsCanvasLayerChild(canvasLayer, prefabPath)) {
            Abort();
        }
    }
}

