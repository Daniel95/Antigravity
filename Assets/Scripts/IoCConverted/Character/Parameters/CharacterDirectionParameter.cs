using UnityEngine;

public class CharacterDirectionParameter {
    public Vector2 MoveDirection;
    public Vector2 CollisionDirection;

    public CharacterDirectionParameter(Vector2 moveDirection, Vector2 collisionDirection) {
        MoveDirection = moveDirection;
        CollisionDirection = collisionDirection;
    }
}
