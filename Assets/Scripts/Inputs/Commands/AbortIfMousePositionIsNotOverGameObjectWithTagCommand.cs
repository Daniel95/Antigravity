using IoCPlus;
using UnityEngine;

public class AbortIfMousePositionIsNotOverGameObjectWithTagCommand : Command<string> {

    [Inject] private LevelEditorSelectedOffGridLevelObjectStatus SelectedOffGridLevelObjectStatus;

    protected override void Execute(string tag) {
        Transform transformOnMousePosition = RaycastHelper.GetTransformOnMousePosition2D();
        if (transformOnMousePosition == null || !transformOnMousePosition.CompareTag(tag)) {
            Abort();
        }
    }

}
