using UnityEngine;

public interface ICharacterCollisionDirection {

    Vector2 CollisionDirection { get; }
    int SavedCollisionsCount { get; }
    void ResetCollisions();

}
