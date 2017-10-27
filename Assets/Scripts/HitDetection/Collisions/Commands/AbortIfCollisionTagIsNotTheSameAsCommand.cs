using IoCPlus;
using UnityEngine;

public class AbortIfCollisionTagIsNotTheSameAsCommand : Command<string> {

    [InjectParameter] private Collision2D collision;

    protected override void Execute(string tag) {
        if(!collision.transform.CompareTag(tag)) {
            Abort();
        }
    }
}
