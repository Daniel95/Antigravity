using IoCPlus;
using UnityEngine;

public class LevelEditorDestroySelectedLevelObjectCommand : Command {

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    protected override void Execute() {
        levelEditorLevelObjectsStatus.LevelObjectsByGameObject.Remove(LevelEditorSelectedLevelObjectStatus.LevelObject);

        View view = LevelEditorSelectedLevelObjectStatus.LevelObject.GetComponent<View>();

        if (view != null) {
            view.Destroy();
        } else {
            Object.Destroy(LevelEditorSelectedLevelObjectStatus.LevelObject);
        }

        LevelEditorSelectedLevelObjectStatus.LevelObject = null;
    }

}
