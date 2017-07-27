using UnityEngine;

public interface ICameraVelocity {

    void Swipe(Vector2 delta);
    void UpdatePreviousTouchScreenPosition(Vector2 screenPosition);
    void AddVelocity(Vector2 velocity);
    void SetVelocity(Vector2 velocity);

}
