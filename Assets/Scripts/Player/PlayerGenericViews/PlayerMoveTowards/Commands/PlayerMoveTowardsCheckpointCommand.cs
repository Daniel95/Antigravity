using IoCPlus;
using UnityEngine;

public class PlayerMoveTowardsCheckpointCommand : Command {

    [Inject] private CheckpointStatus checkPointStatus;

    [Inject(Label.Player)] private Ref<IMoveTowards> playerMoveTowardsRef;

    [Inject] private PlayerMoveTowardsCheckpointCompletedEvent playerMoveTowardsCheckpointCompletedEvent;

    protected override void Execute() {
        playerMoveTowardsRef.Get().StartMovingToTarget(
            checkPointStatus.checkpoint.transform.position, 
            playerMoveTowardsCheckpointCompletedEvent
        );
    }
}
