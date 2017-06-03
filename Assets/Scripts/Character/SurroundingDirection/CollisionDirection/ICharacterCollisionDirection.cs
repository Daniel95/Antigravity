using UnityEngine;

public interface ICharacterCollisionDirection {

    int SavedCollisionsCount { get; }

    Vector2 GetUpdatedCollisionDirection(Collision2D collision);
    Vector2 GetCurrentCollisionDirection();
    void RemoveCollisionDirection(Vector2 collisionDirection);
    void ResetCollisionDirection();
}
