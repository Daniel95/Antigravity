using IoCPlus;
using UnityEngine;

public class DestroyPlayerCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        if(playerStatus.Player == null) {
            Debug.LogWarning("Can't destroy player because player is null.");
            Abort();
            return;
        }

        Object.Destroy(playerStatus.Player);
    }
}
