using IoCPlus;
using UnityEngine;

public class DestroyChildInCanvasLayerCommand : Command<string, CanvasLayer> {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    protected override void Execute(string prefabPath, CanvasLayer canvasLayer) {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab == null) {
            Debug.LogWarning("Can't instantiate view prefab as no prefab is found at given path '" + prefabPath + "'.");
            return;
        }

        ObjectId objectId = prefab.GetComponent<ObjectId>();
        if(objectId == null) {
            Debug.LogWarning("Cant find ObjectId in gameobject of given path '" + prefabPath + "'.");
            return;
        }

        GameObject child = canvasUIRef.Get().GetCanvasLayerChild(objectId.Id, canvasLayer);
        canvasUIRef.Get().DestroyCanvasLayerChild(child, objectId.Id, canvasLayer);
    }
}

