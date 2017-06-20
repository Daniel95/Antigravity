using IoCPlus;
using UnityEngine;

public class AbortIfColliderIsNotACheckpointCommand : Command {

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        if(!collider.transform.CompareTag(Tags.CheckPoint)) {
            Abort();
        }
    }

}

