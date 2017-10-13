using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedOffGridLevelObjectStatusCommand : Command {

    [Inject] private LevelEditorSelectedOffGridLevelObjectStatus SelectedOffGridLevelObjectStatus;

    protected override void Execute() {
        Transform transformOnMousePosition = RaycastHelper.GetTransformOnMousePosition2D();
        if (transformOnMousePosition == null || !transformOnMousePosition.CompareTag("TEMP")) { return; }

        SelectedOffGridLevelObjectStatus.OffGridLevelObject = transformOnMousePosition.gameObject;
    }

}
