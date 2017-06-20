using IoCPlus;

public class AbortIfCheckPointIsNullCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    protected override void Execute() {
        if (checkpointStatus.checkpoint == null) {
            Abort();
        }
    }
}
