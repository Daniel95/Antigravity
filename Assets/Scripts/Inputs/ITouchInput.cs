public interface ITouchInput {

    bool TouchStarted2FingersAfterIdle { get; }
    bool Pinching { get; }
    bool Idling { get; }

}
