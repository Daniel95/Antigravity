using IoCPlus;
using UnityEngine;

public class InstantiateCanvasCommand : Command {

    [Inject] private IContext context;

    [Inject] private CanvasModel canvasModel;

    protected override void Execute() {
        View canvasPrefab = Resources.Load<View>("UI/Canvas");

        if (canvasPrefab == null) {
            Debug.Log("Can't instantiate view prefab as no prefab is found at given path 'UI/Canvas'.");
            return;
        }

        View canvasView = context.InstantiateView(canvasPrefab);
        canvasModel.CanvasGO = canvasView.gameObject;
    }
}
