using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfMousePositionIsOverLevelObjectCommand : Command {

    protected override void Execute() {
        List<Transform> transforms = RaycastHelper.GetTransformOnPosition2D(Input.mousePosition);
        foreach (Transform transform in transforms) {
            GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(transform.name);
            if (generateableLevelObjectNode != null) {
                Abort();
                return;
            }
        }
    }

}
