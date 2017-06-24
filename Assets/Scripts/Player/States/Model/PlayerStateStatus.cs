public class PlayerStateStatus {

    public enum PlayerState {
        Grappling,
        Sliding,
        Floating,
        RevivedAtCheckpoint,
        RevivedAtStart,
    }

    public PlayerState State;
}
