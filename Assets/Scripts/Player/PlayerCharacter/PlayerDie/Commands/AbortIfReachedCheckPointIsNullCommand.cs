using IoCPlus;

public class AbortIfReachedCheckPointIsNullCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    protected override void Execute() {
        if (checkpointStatus.ReachedCheckpoint == null) {
            Abort();
        }
    }
}
