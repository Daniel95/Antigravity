using IoCPlus;
using UnityEngine;

public class SelectionFieldChangedEvent : Signal<SelectionFieldChangedEvent.Parameter> {

    public class Parameter {
        public Vector2 SelectionFieldStartPosition;
        public Vector2 SelectionFieldEndPosition;
    }

}
