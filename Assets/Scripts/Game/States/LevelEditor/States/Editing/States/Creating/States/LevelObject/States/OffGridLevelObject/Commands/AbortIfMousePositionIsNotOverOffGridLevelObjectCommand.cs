using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfMousePositionIsNotOverOffGridLevelObjectCommand : Command {

    protected override void Execute() {
        List<Transform> transforms = RaycastHelper.GetTransformOnMousePosition2D();
        foreach (Transform transform in transforms) {
            GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(transform.name);
            if (generateableLevelObjectNode != null && !generateableLevelObjectNode.OnGrid) {
                return;
            }
        }

        Abort();
    }

}
