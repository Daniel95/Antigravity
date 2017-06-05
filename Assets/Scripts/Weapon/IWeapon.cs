using UnityEngine;

public interface IWeapon {

    GameObject Owner { get; }
    Vector2 ShootDirection { get; set; }
    Vector2 SpawnPosition { get; }

    Vector2 GetShootDestinationPoint(Vector2 direction);
}
