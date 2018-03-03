﻿using IoCPlus;
using UnityEngine;

public class AbortIfScreenPositionContainsGridElementCommand : Command {

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(screenPosition);
        if (LevelEditorGrid.GridPositions.Contains(gridPosition)) {
            Abort();
        }
    }

}
