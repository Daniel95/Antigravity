using IoCPlus;
using System.Collections.Generic;

public class AbortIfPlayerStateStatusStateIsStatesCommand : Command<List<PlayerStateStatus.PlayerState>> {

    [Inject] private PlayerStateStatus playerStateStatus;

    protected override void Execute(List<PlayerStateStatus.PlayerState> playerStates) {
        foreach (PlayerStateStatus.PlayerState playerState in playerStates) {
            if(playerStateStatus.State == playerState) {
                Abort();
            }
        }
    }
}
