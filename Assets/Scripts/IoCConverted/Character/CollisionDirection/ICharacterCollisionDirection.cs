using UnityEngine;

public interface ICharacterCollisionDirection {

    Vector2 GetUpdatedCollisionDirection(Collision2D collision, Vector2 cornersDirection);
    Vector2 GetCurrentCollisionDirection(Vector2 cornersDirection);
    void RemoveCollisionDirection(Vector2 collisionDirection);
    void ResetCollisionDirection();
}
