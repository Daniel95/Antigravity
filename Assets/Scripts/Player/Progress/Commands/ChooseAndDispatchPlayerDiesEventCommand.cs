using IoCPlus;

public class ChooseAndDispatchPlayerDiesEventCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    [Inject] private ReloadSceneEvent reloadSceneEvent;
    [Inject] private RespawnPlayerEvent respawnPlayerEvent;

    protected override void Execute() {
        if(playerStatus.CheckpointReached) {
            respawnPlayerEvent.Dispatch();
        } else {
            reloadSceneEvent.Dispatch();
        }
    }
}
