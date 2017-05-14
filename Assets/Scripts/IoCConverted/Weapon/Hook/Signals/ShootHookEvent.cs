using IoCPlus;
using UnityEngine;

public class ShootHookEvent : Signal<ShootHookEvent.ShootHookEventParameter> {

    public class ShootHookEventParameter {
        public Vector2 Destination;
        public Vector2 StartPosition;

        public ShootHookEventParameter(Vector2 destination, Vector2 startPosition) {
            Destination = destination;
            StartPosition = startPosition;
        }
    }
}
