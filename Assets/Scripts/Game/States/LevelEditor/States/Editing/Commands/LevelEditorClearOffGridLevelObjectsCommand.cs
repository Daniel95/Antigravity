using IoCPlus;
using UnityEngine;

public class LevelEditorClearOffGridLevelObjectsCommand : Command {

    [Inject] private LevelEditorOffGridLevelObjectsStatus offGridLevelObjectsStatus;

    protected override void Execute() {
        foreach (GameObject offGridLevelObjectGameObject in offGridLevelObjectsStatus.OffGridLevelObjectsByGameObject.Keys) {
            Object.Destroy(offGridLevelObjectGameObject);
        }
        offGridLevelObjectsStatus.OffGridLevelObjectsByGameObject.Clear();
    }

}
