using UnityEngine;

public interface ICharacterCollisionDirection {

    Vector2 GetUpdatedCollisionDirection(Collision2D collision);
    Vector2 GetCurrentCollisionDirection();
    void RemoveCollisionDirection(Vector2 collisionDirection);
    void ResetCollisionDirection();
}
