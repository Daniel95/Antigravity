using IoCPlus;

public class AbortIfCheckPointIsNullCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    protected override void Execute() {
        if (checkpointStatus == null) {
            Abort();
        }
    }
}
