using IoCPlus;
using UnityEngine;

public class AbortIfCollider2DIsNotCheckpointCommand : Command {

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        if(!collider.transform.CompareTag(Tags.CheckPoint)) {
            Abort();
        }
    }

}

