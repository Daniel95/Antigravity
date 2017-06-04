using IoCPlus;

public class MoveTowardsCheckpointCommand : Command {

    [Inject] private CheckpointStatus checkPointStatus;

    [Inject(Label.Player)] private Ref<IMoveTowards> moveTowardsRef;

    [Inject] private MoveTowardsCheckpointCompletedEvent moveTowardsCheckpointCompletedEvent;

    protected override void Execute() {
        moveTowardsRef.Get().StartMovingToTarget(
            checkPointStatus.checkpoint.transform.position, 
            moveTowardsCheckpointCompletedEvent
        );
    }
}
