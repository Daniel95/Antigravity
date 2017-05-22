using IoCPlus;

public class ChooseAndDispatchPlayerDiesEventCommand : Command {

    [Inject] private PlayerModel playerModel;

    [Inject] private ReloadSceneEvent reloadSceneEvent;
    [Inject] private RespawnPlayerEvent respawnPlayerEvent;

    protected override void Execute() {
        if(playerModel.CheckpointReached) {
            respawnPlayerEvent.Dispatch();
        } else {
            reloadSceneEvent.Dispatch();
        }
    }
}
