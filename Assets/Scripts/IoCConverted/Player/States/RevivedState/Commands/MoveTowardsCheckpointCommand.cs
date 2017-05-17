using IoCPlus;

public class MoveTowardsCheckpointCommand : Command {

    [Inject] private CheckpointStatus checkPointStatus;

    [Inject(Label.Player)] private Ref<IMoveTowards> moveTowardsRef;

    protected override void Execute() {
        moveTowardsRef.Get().StartMoving(checkPointStatus.checkpoint.transform.position);
    }
}
