using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedOffGridLevelObjectStatusCommand : Command {

    protected override void Execute() {
        Transform transformOnMousePosition = RaycastHelper.GetTransformOnMousePosition2D();
        if (transformOnMousePosition == null || GenerateableLevelObjectLibrary.Contains(transformOnMousePosition.name)) { return; }

        LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject = transformOnMousePosition.gameObject;
    }

}
