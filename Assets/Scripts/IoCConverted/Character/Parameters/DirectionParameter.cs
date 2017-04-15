using UnityEngine;

public class DirectionParameter {
    public Vector2 MoveDirection;
    public Vector2 CollisionDirection;

    public DirectionParameter(Vector2 moveDirection, Vector2 collisionDirection) {
        MoveDirection = moveDirection;
        CollisionDirection = collisionDirection;
    }
}
