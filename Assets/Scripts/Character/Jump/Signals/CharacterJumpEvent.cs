using IoCPlus;
using UnityEngine;

public class CharacterJumpEvent : Signal<CharacterJumpEvent.Parameter> {
    
    public class Parameter {
        public Vector2 MoveDirection;
        public Vector2 CollisionDirection;
        public Vector2 CenterRaycastDirection;

        public Parameter(Vector2 moveDirection, Vector2 collisionDirection, Vector2 centerRaycastDirection) {
            MoveDirection = moveDirection;
            CenterRaycastDirection = centerRaycastDirection;

            if (collisionDirection != Vector2.zero) {
                CollisionDirection = collisionDirection;
            } else {
                CollisionDirection = centerRaycastDirection;
            }
        }
    }
}
