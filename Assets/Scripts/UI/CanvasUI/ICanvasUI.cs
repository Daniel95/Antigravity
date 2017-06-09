using IoCPlus;
using UnityEngine;

public interface ICanvasUI {

    Transform GetCanvasLayerTransform(CanvasLayer canvasLayer);
    void AddViewToCanvasLayer(View child, CanvasLayer layer);
    void RemoveCanvasLayerContentView(View view, CanvasLayer canvasLayer);
    View GetCanvasLayerContentView(string name, CanvasLayer canvasLayer);

}
