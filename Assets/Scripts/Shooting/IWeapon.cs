using UnityEngine;

public interface IWeapon
{
    void Fire(Vector2 _direction, Vector2 _destination, Vector2 _spawnPosition);
}
