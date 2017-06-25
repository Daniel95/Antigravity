public class PlayerStateStatus {

    public enum PlayerState {
        Grappling,
        Sliding,
        Floating,
        RespawnAtCheckpoint,
        RespawnAtStart,
    }

    public PlayerState State;
}
