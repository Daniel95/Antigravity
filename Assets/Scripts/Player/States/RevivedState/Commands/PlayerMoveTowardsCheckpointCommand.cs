using IoCPlus;
using UnityEngine;

public class PlayerMoveTowardsCheckpointCommand : Command {

    [Inject] private CheckpointStatus checkPointStatus;

    [Inject(Label.Player)] private Ref<IMoveTowards> moveTowardsRef;

    [Inject] private PlayerMoveTowardsCheckpointCompletedEvent moveTowardsCheckpointCompletedEvent;

    protected override void Execute() {
        moveTowardsRef.Get().StartMovingToTarget(
            checkPointStatus.checkpoint.transform.position, 
            moveTowardsCheckpointCompletedEvent
        );
    }
}
