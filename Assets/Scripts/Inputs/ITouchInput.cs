public interface ITouchInput {

    bool TouchStarted2FingersAfterIdle { get; }
    bool Pinching { get; }
    bool SwipeDelta2FingersBiggerThenPinchDelta { get; }
    bool Idling { get; }

}
