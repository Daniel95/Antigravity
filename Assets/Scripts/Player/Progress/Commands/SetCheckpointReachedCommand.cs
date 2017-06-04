using IoCPlus;

public class SetCheckpointReachedCommand : Command<bool> {

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute(bool state) {
        playerStatus.CheckpointReached = state;
    }
}
