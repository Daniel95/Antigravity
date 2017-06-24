using IoCPlus;

public class AbortIfReachedCheckPointIsNotNullCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    protected override void Execute() {
        if (checkpointStatus.ReachedCheckpoint != null) {
            Abort();
        }
    }
}
