using IoCPlus;
using UnityEngine;

public class AbortIfCollisionGameObjectIsNotATileCommand : Command {

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        Debug.Log("is tile " + GenerateableTileLibrary.IsTile(collision.gameObject.name));
        if (!GenerateableTileLibrary.IsTile(collision.gameObject.name)) {
            Abort();
        }
    }

}
