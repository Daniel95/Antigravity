using IoCPlus;
using UnityEngine;

public class LevelEditorDestroySelectedLevelObjectCommand : Command {

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    protected override void Execute() {
        levelEditorLevelObjectsStatus.LevelObjectsByGameObject.Remove(LevelEditorSelectedLevelObjectStatus.LevelObject);
        Object.Destroy(LevelEditorSelectedLevelObjectStatus.LevelObject);
        LevelEditorSelectedLevelObjectStatus.LevelObject = null;
    }

}
