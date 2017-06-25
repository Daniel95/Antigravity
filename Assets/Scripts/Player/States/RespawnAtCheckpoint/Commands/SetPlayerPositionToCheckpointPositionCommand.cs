using IoCPlus;

public class SetPlayerPositionToCheckpointPositionCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;
    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        playerStatus.Player.transform.position = checkpointStatus.ReachedCheckpoint.transform.position;
    }
}
