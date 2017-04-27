using IoCPlus;
using UnityEngine;

public class AbortIfTriggerTagIsNotTheSameCommand : Command<string> {

    [InjectParameter] private Collider2D collider;

    protected override void Execute(string tag) {
        if(!collider.CompareTag(tag)) {
            Abort();
        }
    }
}
