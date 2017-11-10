using IoCPlus;
using UnityEngine;

public class LevelEditorSwipeMovedOnWorldEvent : Signal<LevelEditorSwipeMovedOnWorldEvent.Parameter> {
    
    public class Parameter {

        public Vector2 Position;
        public Vector2 Delta;

    }

}
