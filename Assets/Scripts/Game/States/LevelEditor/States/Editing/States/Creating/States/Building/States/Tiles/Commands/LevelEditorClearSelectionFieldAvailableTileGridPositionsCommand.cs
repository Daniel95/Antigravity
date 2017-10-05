﻿using IoCPlus;
using UnityEngine;

public class LevelEditorClearSelectionFieldAvailableTileGridPositionsCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().ClearSelectionFieldAvailableGridPositions();
    }

}
