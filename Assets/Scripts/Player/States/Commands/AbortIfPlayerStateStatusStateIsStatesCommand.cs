using IoCPlus;
using System.Collections.Generic;

public class AbortIfPlayerStateStatusStateIsStatesCommand : Command<List<PlayerState>> {

    [Inject] private PlayerStateStatus playerStateStatus;

    protected override void Execute(List<PlayerState> playerStates) {
        foreach (PlayerState playerState in playerStates) {
            if(playerStateStatus.State == playerState) {
                Abort();
            }
        }
    }
}
