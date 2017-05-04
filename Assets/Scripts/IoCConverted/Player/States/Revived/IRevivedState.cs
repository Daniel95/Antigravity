interface IRevivedState {

    bool IsReadyForLaunchInput { get; set; }
    bool IsInPosition { get; set; }

    void StartDelayLaunchInput();
}