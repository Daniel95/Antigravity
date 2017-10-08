﻿using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand : Command<LevelObjectTransformType> {

    protected override void Execute(LevelObjectTransformType levelObjectTransformType) {
        if(LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType != levelObjectTransformType) {
            Abort();
        }
    }

}