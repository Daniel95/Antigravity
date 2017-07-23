using IoCPlus;

public class AbortIfPlayerStateStatusStateIsStateCommand : Command<PlayerState> {

    [Inject] private PlayerStateStatus playerStateStatus;

    protected override void Execute(PlayerState playerState) {
        if(playerStateStatus.State == playerState) {
            Abort();
        }
    }
}
