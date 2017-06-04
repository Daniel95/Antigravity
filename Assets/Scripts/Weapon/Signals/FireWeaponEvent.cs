using IoCPlus;
using UnityEngine;

public class FireWeaponEvent : Signal<FireWeaponEvent.Parameter> {

    public class Parameter {
        public Vector2 Direction;
        public Vector2 StartPosition;

        public Parameter(Vector2 destination, Vector2 startPosition) {
            Direction = destination;
            StartPosition = startPosition;
        }
    }
}