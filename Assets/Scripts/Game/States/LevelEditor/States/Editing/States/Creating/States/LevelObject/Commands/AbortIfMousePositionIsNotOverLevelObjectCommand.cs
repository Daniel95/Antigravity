using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfMousePositionIsNotOverLevelObjectCommand : Command {

    protected override void Execute() {
        List<Transform> transforms = RaycastHelper.GetTransformOnPosition2D(Input.mousePosition);
        foreach (Transform transform in transforms) {
            bool isLevelObject = GenerateableLevelObjectLibrary.IsLevelObject(transform.name);
            if (isLevelObject) {
                return;
            }
        }

        Abort();
    }

}
