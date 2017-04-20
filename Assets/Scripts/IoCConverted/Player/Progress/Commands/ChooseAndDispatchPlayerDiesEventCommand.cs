using IoCPlus;

public class ChooseAndDispatchPlayerDiesEventCommand : Command {

    [Inject] private PlayerModel playerModel;

    [Inject] private ReloadSceneEvent reloadSceneEvent;
    [Inject] private RespawnPlayerEvent respawnPlayerEvent;

    protected override void Execute() {
        if(playerModel.checkpointReached) {
            respawnPlayerEvent.Dispatch();
        } else {
            reloadSceneEvent.Dispatch();
        }
    }
}
