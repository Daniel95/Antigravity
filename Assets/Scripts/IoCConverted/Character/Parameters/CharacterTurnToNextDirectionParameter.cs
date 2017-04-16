using UnityEngine;

public class CharacterTurnToNextDirectionParameter {

    public Vector2 MoveDirection;
    public Vector2 CollisionDirection;
    public Vector2 CornerDirection;

    public CharacterTurnToNextDirectionParameter(Vector2 moveDirection, Vector2 collisionDirection, Vector2 cornersRaycastDirection) {
        MoveDirection = moveDirection;
        CollisionDirection = collisionDirection;

        if (CheckIsCorner(collisionDirection)) {
            CornerDirection = collisionDirection;
        } else if (CheckIsCorner(cornersRaycastDirection)) {
            CornerDirection = cornersRaycastDirection;
        } else {
            CornerDirection = Vector2.zero;
        } 
    }

    private static bool CheckIsCorner(Vector2 direction) {
        return direction.x != 0 && direction.y != 0;
    }
}
