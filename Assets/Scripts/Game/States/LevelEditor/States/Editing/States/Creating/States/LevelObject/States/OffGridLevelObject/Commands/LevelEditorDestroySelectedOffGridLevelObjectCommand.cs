using IoCPlus;
using UnityEngine;

public class LevelEditorDestroySelectedOffGridLevelObjectCommand : Command {

    protected override void Execute() {
        Object.Destroy(LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject);
        LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject = null;
    }

}
