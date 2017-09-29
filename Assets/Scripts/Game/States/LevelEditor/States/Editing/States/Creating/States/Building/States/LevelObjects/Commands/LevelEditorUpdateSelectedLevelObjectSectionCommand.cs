﻿using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectSectionCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectSectionStatus levelEditorSelectedLevelObjectSectionStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelObjectSection levelObjectSection = LevelEditorLevelObjectSectionGrid.Instance.GetLevelObjectSection(gridPosition);
        levelEditorSelectedLevelObjectSectionStatus.LevelObjectSection = levelObjectSection;
        levelEditorSelectedLevelObjectSectionStatus.GridPosition = gridPosition;
    }

}
