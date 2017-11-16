using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorSelectedLevelObjectDoesNotCollideWithLevelObjectsCommand : Command {

    protected override void Execute() {
        bool collidingWithLevelObject =  LevelEditorSelectedLevelObjectStatus.CollisionColliders.Exists(x => GenerateableLevelObjectLibrary.IsLevelObject(x.name));
        if (!collidingWithLevelObject) {
            Abort();
        }
    }

}
