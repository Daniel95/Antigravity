using IoCPlus;
using UnityEngine;

public class AbortIfCollisionIsALevelObjectCommand : Command {

    [InjectParameter] private Collision2D collision2D;

    protected override void Execute() {
        bool isLevelObject = GenerateableLevelObjectLibrary.IsLevelObject(collision2D.transform.name);
        if (isLevelObject) {
            Abort();
        }
    }

}
