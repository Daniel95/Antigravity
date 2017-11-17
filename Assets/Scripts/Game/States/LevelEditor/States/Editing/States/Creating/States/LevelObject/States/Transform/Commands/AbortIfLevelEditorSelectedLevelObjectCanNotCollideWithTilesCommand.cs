﻿using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectCannotCollideWithTilesCommand : Command {

    protected override void Execute() {
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(LevelEditorSelectedLevelObjectStatus.LevelObject.name);
        if (!generateableLevelObjectNode.CanCollideWithTiles) {
            Abort();
        }
    }

}
