using IoCPlus;
using UnityEngine;

public class DestroyListenerViewInCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer canvasLayer) {
        ListenerView prefab = Resources.Load<ListenerView>(prefabPath);
        if (prefab == null) {
            Debug.LogWarning("No ListenerView is found at given path '" + prefabPath + "'.", prefab);
            return;
        }

        GameObject child = canvasUIRef.Get().GetCanvasLayerChild(canvasLayer, prefabPath);
        ListenerView listenerView = child.GetComponent<ListenerView>();
        listenerView.Signal = null;
        canvasUIRef.Get().DestroyCanvasLayerChild(child, canvasLayer, prefabPath);
    }
}

