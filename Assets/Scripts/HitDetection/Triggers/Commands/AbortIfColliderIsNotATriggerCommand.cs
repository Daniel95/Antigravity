using IoCPlus;
using UnityEngine;

public class AbortIfColliderIsNotATriggerCommand : Command {

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        if (!collider.isTrigger) {
            Abort();
        }
    }
}
