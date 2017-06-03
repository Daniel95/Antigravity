using IoCPlus;
using UnityEngine;

public class AbortIfCollider2DIsNotACheckpointCommand : Command {

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        if(!collider.transform.CompareTag(Tags.CheckPoint)) {
            Abort();
        }
    }

}

