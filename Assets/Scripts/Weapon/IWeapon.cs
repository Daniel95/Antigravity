using UnityEngine;

public interface IWeapon {

    Vector2 GetShootDestinationPoint(Vector2 direction);
    Vector2 SpawnPosition { get; }
}
