using IoCPlus;

public class MoveTowardsCheckpointCommand : Command {

    [Inject] private CheckpointStatus checkPointStatus;

    [Inject("Player/MoveTowards")] private Ref<IMoveTowards> moveTowardsRef;

    protected override void Execute() {
        moveTowardsRef.Get().StartMoving(checkPointStatus.checkpoint.transform.position);
    }
}
