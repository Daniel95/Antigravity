using IoCPlus;

public class SetPlayerStateStatusCommand : Command<PlayerStateStatus.PlayerState> {

    [Inject] private PlayerStateStatus playerStateStatus;

    protected override void Execute(PlayerStateStatus.PlayerState playerState) {
        playerStateStatus.State = playerState;
    }
}
