using IoCPlus;
using UnityEngine;

public class LevelEditorClearLevelObjectsCommand : Command {

    [Inject] private LevelEditorLevelObjectsStatus levelObjectsStatus;

    protected override void Execute() {
        foreach (GameObject levelObjectGameObject in levelObjectsStatus.LevelObjectsByGameObject.Keys) {
            Object.Destroy(levelObjectGameObject);
        }
        levelObjectsStatus.LevelObjectsByGameObject.Clear();
    }

}
