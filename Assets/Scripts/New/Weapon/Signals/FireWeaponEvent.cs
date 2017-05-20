using IoCPlus;
using UnityEngine;

public class FireWeaponEvent : Signal<FireWeaponEvent.FireWeapFireWeaponEventParameter> {

    public class FireWeapFireWeaponEventParameter {
        public Vector2 Destination;
        public Vector2 StartPosition;

        public FireWeapFireWeaponEventParameter(Vector2 destination, Vector2 startPosition) {
            Destination = destination;
            StartPosition = startPosition;
        }
    }
}