using UnityEngine;

public class CharacterJumpParameter {

    public Vector2 MoveDirection;
    public Vector2 CollisionDirection;
    public Vector2 RaycastDirection;

    public CharacterJumpParameter(Vector2 moveDirection, Vector2 collisionDirection, Vector2 raycastDirection) {
        MoveDirection = moveDirection;
        CollisionDirection = collisionDirection;
        RaycastDirection = raycastDirection;
    }
}
