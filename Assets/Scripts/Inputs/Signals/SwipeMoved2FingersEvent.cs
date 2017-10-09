using IoCPlus;
using UnityEngine;

public class SwipeMoved2FingersEvent : Signal<SwipeMoved2FingersEvent.Parameter> {

    public class Parameter {
        public Vector2 DeltaPosition;
        public Vector2 Position;
    }

}