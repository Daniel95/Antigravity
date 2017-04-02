using UnityEngine;

public interface IWeaponInput {

    void ReleaseInDirection(Vector2 dir);
    void Dragging(Vector2 dir);
    void CancelDragging();
}
