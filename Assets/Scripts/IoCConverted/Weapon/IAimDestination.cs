using UnityEngine;

public interface IAimDestination {

    Vector2 GetDestinationPoint(Vector2 direction);
    Vector2 SpawnPosition { get; }
}
