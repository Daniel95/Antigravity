using IoCPlus;
using UnityEngine;

public class LevelEditorDestroySelectedOffGridLevelObjectCommand : Command {

    [Inject] private LevelEditorOffGridLevelObjectsStatus levelEditorOffGridLevelObjectsStatus;

    protected override void Execute() {
        levelEditorOffGridLevelObjectsStatus.OffGridLevelObjectsByGameObject.Remove(LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject);
        Object.Destroy(LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject);
        LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject = null;
    }

}
