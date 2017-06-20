using IoCPlus;

public class AbortIfCheckPointIsNotNullCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    protected override void Execute() {
        if (checkpointStatus != null) {
            Abort();
        }
    }
}
