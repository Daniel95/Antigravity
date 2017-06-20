using IoCPlus;

public class AbortIfCheckPointIsNotNullCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    protected override void Execute() {
        if (checkpointStatus.checkpoint != null) {
            Abort();
        }
    }
}
