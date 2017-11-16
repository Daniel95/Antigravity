﻿using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorColliderIsALevelObjectCommand : Command {

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        bool isLevelObject = GenerateableLevelObjectLibrary.IsLevelObject(collider.transform.name);
        if(isLevelObject) {
            Abort();
        }
    }

}
