using System;
using UnityEngine;

public interface ICanvasUI {

    Transform GetCanvasLayerTransform(CanvasLayer canvasLayer);
    void AddChildToCanvasLayer(GameObject child, CanvasLayer layer, string key);
    void DestroyCanvasLayerChild(GameObject child, CanvasLayer canvasLayer, string key, Action onDestroyCompleted = null);
    bool ContainsCanvasLayerChild(CanvasLayer canvasLayer, string key);
    GameObject GetCanvasLayerChild(CanvasLayer canvasLayer, string key);

}
