using UnityEngine;

public interface ICharacterCollisionDirection {

    int SavedCollisionsCount { get; }

    Vector2 UpdateCollisionDirection(Collision2D collision);
    Vector2 GetCollisionDirection();
    void RemoveSavedCollisionDirection(Vector2 collisionDirection);
    void RemoveSavedCollider(Collision2D collider);
    void ResetSavedCollisions();
}
