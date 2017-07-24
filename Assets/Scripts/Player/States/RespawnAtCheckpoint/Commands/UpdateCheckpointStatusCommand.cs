using IoCPlus;
using UnityEngine;

public class UpdateCheckpointStatusCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    [Inject] private Refs<ICheckpoint> checkpointRefs;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        GameObject colliderGameObject = collider.gameObject;

        ICheckpoint checkpoint = checkpointRefs.Find(x => {
            bool collidedWithThisCheckpoint = colliderGameObject == x.CheckpointGameObject || colliderGameObject == x.CheckpointBoundaryGameObject;
            return collidedWithThisCheckpoint;
        });

        if(!checkpoint.Unlocked) {
            checkpoint.CheckpointUnlockedEffect();
            checkpoint.Unlocked = true;
        }

        checkpointStatus.ReachedCheckpoint = checkpoint.CheckpointGameObject;
    }
}
