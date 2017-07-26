using IoCPlus;
using UnityEngine;

public class SwipeMovedEvent : Signal<SwipeMovedEvent.Parameter> {

    public class Parameter {
        public Vector2 DeltaPosition;
        public Vector2 Position;
    }

}