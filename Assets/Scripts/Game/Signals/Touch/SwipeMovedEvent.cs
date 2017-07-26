using IoCPlus;
using UnityEngine;

public class SwipeMovedEvent : Signal<SwipeMovedEvent.DeltaPosition, SwipeMovedEvent.MovePosition> {
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