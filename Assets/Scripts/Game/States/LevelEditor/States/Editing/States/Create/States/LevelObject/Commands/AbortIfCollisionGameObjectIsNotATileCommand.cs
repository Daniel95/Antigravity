using IoCPlus;
using UnityEngine;

public class AbortIfCollisionGameObjectIsNotATileCommand : Command {

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        if (!GenerateableTileLibrary.IsTile(collision.gameObject.name)) {
            Abort();
        }
    }

}
