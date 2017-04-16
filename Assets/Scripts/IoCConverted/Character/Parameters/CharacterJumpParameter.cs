using UnityEngine;

public class CharacterJumpParameter {

    public Vector2 MoveDirection;
    public Vector2 CollisionDirection;
    public Vector2 CenterRaycastDirection;

    public CharacterJumpParameter(Vector2 moveDirection, Vector2 collisionDirection, Vector2 centerRaycastDirection) {
        MoveDirection = moveDirection;
        CenterRaycastDirection = centerRaycastDirection;

        if (collisionDirection != Vector2.zero) {
            CollisionDirection = collisionDirection;
        } else {
            CollisionDirection = centerRaycastDirection;
        }
    }
}
