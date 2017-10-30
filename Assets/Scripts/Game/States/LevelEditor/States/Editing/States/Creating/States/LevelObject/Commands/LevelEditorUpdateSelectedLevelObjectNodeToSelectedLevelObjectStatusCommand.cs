using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectNodeToSelectedLevelObjectStatusCommand : Command {

    protected override void Execute() {
        GameObject levelObject = LevelEditorSelectedLevelObjectStatus.LevelObject;
        LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObject.name);
    }


}
