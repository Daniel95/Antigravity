using IoCPlus;
using UnityEngine;

public class SwipeMovedOnWorldEvent : Signal<SwipeMovedOnWorldEvent.Parameter> {
    
    public class Parameter {
        public Vector2 Position;
        public Vector2 Delta;
    }

}
