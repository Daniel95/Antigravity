using IoCPlus;
using UnityEngine;

public class AbortIfCollisionGameObjectIsNotALevelObjectCommand : Command {

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        if(!GenerateableLevelObjectLibrary.IsLevelObject(collision.gameObject.name)) {
            Abort();
        }
    }

}
