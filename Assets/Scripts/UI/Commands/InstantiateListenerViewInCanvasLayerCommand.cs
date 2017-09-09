using IoCPlus;
using UnityEngine;

public class InstantiateListenerViewInCanvasLayerCommand : Command<string, CanvasLayer, Signal> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer canvasLayer, Signal signal) {
        ListenerView prefab = Resources.Load<ListenerView>(prefabPath);
        if (prefab == null) {
            Debug.LogWarning("Can't instantiate listener view prefab as no listener view is found at given path '" + prefabPath + "'.");
            return;
        }
        ListenerView listenerView = context.InstantiateView(prefab);
        listenerView.Signal = signal;
        canvasUIRef.Get().AddChildToCanvasLayer(listenerView.gameObject, canvasLayer, prefabPath);
    }
}
