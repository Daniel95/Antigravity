using IoCPlus;
using UnityEngine;

public class DebugLogCollider2DCommand : Command {

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        Debug.Log("Trigger with: " + collider.name);
    }
}
