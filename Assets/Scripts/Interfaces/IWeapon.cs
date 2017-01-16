using UnityEngine;

public interface IWeapon
{
    void Dragging(Vector2 destination, Vector2 _spawnPosition);

    void Release(Vector2 destination, Vector2 spawnPosition);

    void CancelDragging();
}
