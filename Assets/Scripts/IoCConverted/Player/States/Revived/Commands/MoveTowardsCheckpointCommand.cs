using IoCPlus;

public class MoveTowardsCheckpointCommand : Command {

    [Inject] private CheckpointStatus checkPointStatus;

    [Inject] private Ref<IMoveTowards> moveTowards;

    protected override void Execute() {
        moveTowards.Get().StartMoving(checkPointStatus.checkpoint.transform.position);
    }
}
