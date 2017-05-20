public interface ITime {

    bool IsPaused { get; }
    void PauseTime(bool pause);
}
