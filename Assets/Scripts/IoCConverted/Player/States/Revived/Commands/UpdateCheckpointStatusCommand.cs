using IoCPlus;
using UnityEngine;

public class UpdateCheckpointStatusCommand : Command {

    [Inject] private CheckpointStatus checkpointStatus;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        checkpointStatus.checkpoint = collider.gameObject;
    }
}
