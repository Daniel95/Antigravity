using UnityEngine;

public interface IWeapon
{
    void Dragging(Vector2 _destination, Vector2 _spawnPosition);

    void Release(Vector2 _destination, Vector2 _spawnPosition);

    void Cancel();
}
