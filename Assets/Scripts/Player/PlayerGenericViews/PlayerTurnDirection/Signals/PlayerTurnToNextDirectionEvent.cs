using IoCPlus;
using UnityEngine;

public class PlayerTurnToNextDirectionEvent : Signal<PlayerTurnToNextDirectionEvent.Parameter> {
    
    public class Parameter {
        public Vector2 MoveDirection;
        public Vector2 SurroundingsDirection;

        public Parameter(Vector2 moveDirection, Vector2 surroundingsDirection) {
            MoveDirection = moveDirection;
            SurroundingsDirection = surroundingsDirection;
        }
    }

}
