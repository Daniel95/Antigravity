using IoCPlus;
using UnityEngine;

public class LevelEditorSelectionFieldChangedEvent : Signal<LevelEditorSelectionFieldChangedEvent.Parameter> {

    public class Parameter {
        public Vector2 SelectionFieldStartPosition;
        public Vector2 SelectionFieldEndPosition;
    }

}
