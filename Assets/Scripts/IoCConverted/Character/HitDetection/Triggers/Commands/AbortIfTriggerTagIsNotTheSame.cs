using IoCPlus;
using UnityEngine;

public class AbortIfTriggerTagIsNotTheSame : Command<string> {

    [InjectParameter] private Collider2D collider;

    protected override void Execute(string tag) {
        if(!collider.CompareTag(tag)) {
            Abort();
        }
    }
}
