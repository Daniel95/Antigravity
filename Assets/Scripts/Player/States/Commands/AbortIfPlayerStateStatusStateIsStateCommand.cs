using IoCPlus;

public class AbortIfPlayerStateStatusStateIsStateCommand : Command<PlayerStateStatus.PlayerState> {

    [Inject] private PlayerStateStatus playerStateStatus;

    protected override void Execute(PlayerStateStatus.PlayerState playerState) {
        if(playerStateStatus.State == playerState) {
            Abort();
        }
    }
}
