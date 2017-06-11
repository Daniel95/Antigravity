using IoCPlus;

public class ChooseAndDispatchPlayerDiesEventCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    [Inject] private GoToCurrentSceneEvent reloadSceneEvent;
    [Inject] private RespawnPlayerEvent respawnPlayerEvent;

    protected override void Execute() {
        if(playerStatus.CheckpointReached) {
            respawnPlayerEvent.Dispatch();
        } else {
            reloadSceneEvent.Dispatch();
        }
    }
}
