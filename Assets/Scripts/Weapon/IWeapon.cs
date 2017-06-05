using UnityEngine;

public interface IWeapon {

    Vector2 SpawnPosition { get; }

    Vector2 GetShootDestinationPoint(Vector2 direction);
}
