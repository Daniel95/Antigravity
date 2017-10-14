﻿using IoCPlus;
using UnityEngine;

public class LevelEditorOffGridLevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<TouchDownEvent>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(false);

    }

}