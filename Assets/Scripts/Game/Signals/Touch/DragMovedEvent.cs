using IoCPlus;
using UnityEngine;

public class DragMovedEvent : Signal<DragMovedEvent.DeltaPosition, DragMovedEvent.MovePosition> {
    public class DeltaPosition : Position {
        public DeltaPosition(Vector2 vector) : base(vector) { }
    }
    public class MovePosition : Position {
        public MovePosition(Vector2 vector) : base(vector) { }
    }
    public class Position {
        public readonly Vector2 Vector;
        public Position(Vector2 vector) {
            Vector = vector;
        }
    }
}