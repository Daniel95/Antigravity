using UnityEngine;

public interface ICharacterCollisionDirection {

    int SavedCollisionsCount { get; }

    Vector2 UpdateCollisionDirection(Collision2D collision);
    Vector2 GetCollisionDirection();
    void RemoveCollisionDirection(Vector2 collisionDirection);
    void ResetCollisionDirection();
}
