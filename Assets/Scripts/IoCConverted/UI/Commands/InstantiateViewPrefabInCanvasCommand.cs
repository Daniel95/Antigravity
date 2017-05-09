using IoCPlus;
using UnityEngine;

public class InstantiateViewPrefabInCanvasCommand : Command<string> {

    [Inject] IContext context;

    [Inject] private CanvasModel canvasModel;

    protected override void Execute(string prefabPath) {
        View prefab = Resources.Load<View>(prefabPath);

        if (prefab == null) {
            Debug.Log("Can't instantiate view prefab as no prefab is found at given path '" + prefabPath + "'.");
            return;
        }

        View viewInstance = context.InstantiateView(prefab);
        viewInstance.transform.SetParent(canvasModel.CanvasGO.transform, false);
    }

}