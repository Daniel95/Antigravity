using IoCPlus;
using UnityEngine;

public class DragMovedEvent : Signal<DragMovedEvent.Parameter> {

    public class Parameter {
        public Vector2 DeltaPosition;
        public Vector2 Position;
    }

}