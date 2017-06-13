using System;
using UnityEngine;

public interface ICanvasUI {

    Transform GetCanvasLayerTransform(CanvasLayer canvasLayer);
    void AddChildToCanvasLayer(GameObject child, CanvasLayer layer);
    void DestroyCanvasLayerChild(GameObject child, string id, CanvasLayer canvasLayer, Action onDestroyCompleted = null);
    GameObject GetCanvasLayerChild(string id, CanvasLayer canvasLayer);

}
