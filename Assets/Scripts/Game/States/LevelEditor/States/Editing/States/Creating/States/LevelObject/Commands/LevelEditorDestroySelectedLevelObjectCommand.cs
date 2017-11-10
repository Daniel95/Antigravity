using IoCPlus;
using UnityEngine;

public class LevelEditorDestroySelectedLevelObjectCommand : Command {

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    protected override void Execute() {
        GameObject levelObject = LevelEditorSelectedLevelObjectStatus.LevelObject;
        levelEditorLevelObjectsStatus.DestroyLevelObject(levelObject);
        LevelEditorSelectedLevelObjectStatus.LevelObject = null;
    }

}
