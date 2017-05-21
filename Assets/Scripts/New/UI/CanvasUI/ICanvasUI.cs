using UnityEngine;

public interface ICanvasUI {

    Vector2 ScreenToCanvas(Vector2 screenPosition);
    Vector2 ScaleToCanvas(Vector2 scale);
    float GetCanvasScale();

    void AddChild(GameObject child, CanvasLayer layer);

}
