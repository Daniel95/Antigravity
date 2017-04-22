using UnityEngine;

public interface IShoot {

    Vector2 GetDestinationPoint(Vector2 direction);
    Vector2 SpawnPosition { get; }
}
