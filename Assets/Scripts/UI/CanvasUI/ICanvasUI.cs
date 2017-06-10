using IoCPlus;
using System;
using UnityEngine;

public interface ICanvasUI {

    Transform GetCanvasLayerTransform(CanvasLayer canvasLayer);
    void AddViewToCanvasLayer(View child, CanvasLayer layer);
    void DestroyCanvasLayerView(View view, CanvasLayer canvasLayer, Action onDestroyCompleted = null);
    View GetCanvasLayerView(string name, CanvasLayer canvasLayer);

}
