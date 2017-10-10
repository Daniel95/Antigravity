public interface ITouchInput {

    bool TouchStarted2FingersAfterIdle { get; }
    bool Pinching { get; }
    bool SwipeMoving2Fingers { get; }
    bool Idling { get; }

}
