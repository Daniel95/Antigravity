using UnityEngine;

public class CharacterBounceParameter {
    public Vector2 MoveDirection;
    public Vector2 CollisionDirection;

    public CharacterBounceParameter(Vector2 moveDirection, Vector2 collisionDirection) {
        MoveDirection = moveDirection;
        CollisionDirection = collisionDirection;
    }
}
