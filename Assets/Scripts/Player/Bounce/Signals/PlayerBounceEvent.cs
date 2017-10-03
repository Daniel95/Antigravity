using IoCPlus;
using UnityEngine;

public class PlayerBounceEvent : Signal<PlayerBounceEvent.Parameter> {

    public class Parameter {
        public Vector2 MoveDirection;
        public Vector2 CollisionDirection;

        public Parameter(Vector2 moveDirection, Vector2 collisionDirection) {
            MoveDirection = moveDirection;
            CollisionDirection = collisionDirection;
        }
    }

}
