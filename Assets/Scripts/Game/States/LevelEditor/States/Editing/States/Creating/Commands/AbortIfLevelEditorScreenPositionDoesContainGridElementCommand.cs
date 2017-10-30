﻿using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorScreenPositionDoesContainGridElementCommand : Command {

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(screenPosition);
        if (LevelEditorGridPositions.GridPositions.Contains(gridPosition)) {
            Abort();
        }
    }

}
