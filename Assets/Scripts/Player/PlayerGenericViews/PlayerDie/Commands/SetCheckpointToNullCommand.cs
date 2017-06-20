using IoCPlus;

public class SetCheckpointToNullCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    protected override void Execute() {
        checkpointStatus.checkpoint = null;
    }
}
