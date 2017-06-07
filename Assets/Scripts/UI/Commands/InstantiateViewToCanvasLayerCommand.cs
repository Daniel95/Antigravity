using IoCPlus;
using UnityEngine;

public class InstantiateViewToCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer layer) {
        View prefab = Resources.Load<View>(prefabPath);
        View view = context.InstantiateView(prefab);
        canvasUIRef.Get().AddChild(view.GetGameObject(), layer);
    }
}
