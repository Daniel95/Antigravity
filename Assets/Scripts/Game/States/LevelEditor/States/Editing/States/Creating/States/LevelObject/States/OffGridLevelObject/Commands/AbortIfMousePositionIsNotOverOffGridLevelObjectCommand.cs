using IoCPlus;
using UnityEngine;

public class AbortIfMousePositionIsNotOverOffGridLevelObjectCommand : Command {

    protected override void Execute() {
        Transform transformOnMousePosition = RaycastHelper.GetTransformOnMousePosition2D();

        if (transformOnMousePosition == null) {
            Abort();
            return;
        }
        if (!GenerateableLevelObjectLibrary.Contains(transformOnMousePosition.name)) {
            Abort();
            return;
        }
        if (GenerateableLevelObjectLibrary.GetNode(transformOnMousePosition.name).OnGrid) {
            Abort();
            return;
        }
    }

}
