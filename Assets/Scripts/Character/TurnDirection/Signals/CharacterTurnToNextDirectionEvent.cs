using IoCPlus;
using UnityEngine;

public class CharacterTurnToNextDirectionEvent : Signal<CharacterTurnToNextDirectionEvent.Parameter> {
    
    public class Parameter {
        public Vector2 MoveDirection;
        public Vector2 CollisionDirection;
        public Vector2 CornerDirection;

        public Parameter(Vector2 moveDirection, Vector2 surroundingsDirection, Vector2 cornersRaycastDirection) {
            MoveDirection = moveDirection;
            CollisionDirection = surroundingsDirection;

            if (CheckIsCorner(surroundingsDirection)) {
                CornerDirection = surroundingsDirection;
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
}
