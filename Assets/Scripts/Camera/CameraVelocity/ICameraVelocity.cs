using UnityEngine;

public interface ICameraVelocity {

    void ResetPosition();
    void StartSwipe(Vector2 touchScreenPosition);
    void Swipe(Vector2 touchScreenPosition);
    void Zoom(Vector2 position, float delta);
    void UpdatePreviousTouchScreenPosition(Vector2 screenPosition);
    void AddVelocity(Vector2 velocity);
    void SetVelocity(Vector2 velocity);

}
