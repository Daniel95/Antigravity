using IoCPlus;
using UnityEngine;

public class ShootHookEvent : Signal<ShootHookEvent.Parameter> {

    public class Parameter {
        public Vector2 Direction;
        public Vector2 StartPosition;

        public Parameter(Vector2 direction, Vector2 startPosition) {
            Direction = direction;
            StartPosition = startPosition;
        }
    }
}
