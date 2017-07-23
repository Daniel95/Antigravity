using IoCPlus;

public class SetPlayerStateStatusCommand : Command<PlayerState> {

    [Inject] private PlayerStateStatus playerStateStatus;

    protected override void Execute(PlayerState playerState) {
        playerStateStatus.State = playerState;
    }
}
