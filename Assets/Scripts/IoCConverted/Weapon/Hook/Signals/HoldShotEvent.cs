using IoCPlus;
using UnityEngine;

public class HoldShotEvent : Signal<HoldShotEvent.HoldShotEventParameter> {

    public class HoldShotEventParameter {
        public Vector2 Destination;
        public Vector2 StartPosition;

        public HoldShotEventParameter(Vector2 destination, Vector2 startPosition) {
            Destination = destination;
            StartPosition = startPosition;
        }
    }
}
