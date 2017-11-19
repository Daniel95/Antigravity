﻿using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectCannotCollideWithTilesCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectRef.Get().LevelObjectType;
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
        if (!generateableLevelObjectNode.CanCollideWithTiles) {
            Abort();
        }
    }

}
