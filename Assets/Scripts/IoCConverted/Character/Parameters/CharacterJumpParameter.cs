using UnityEngine;

public class CharacterJumpParameter {

    public Vector2 MoveDirection;
    public Vector2 CollisionDirection;
    public Vector2 RaycastDirection;

    public CharacterJumpParameter(Vector2 moveDirection, Vector2 collisionDirection, Vector2 raycastDirection) {
        MoveDirection = moveDirection;
        RaycastDirection = raycastDirection;

        if (collisionDirection != Vector2.zero) {
            CollisionDirection = collisionDirection;
        } else {
            CollisionDirection = raycastDirection;
        }
    }
}
