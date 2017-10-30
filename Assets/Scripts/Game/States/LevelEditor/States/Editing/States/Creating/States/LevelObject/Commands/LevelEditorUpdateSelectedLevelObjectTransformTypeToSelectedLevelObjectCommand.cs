using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand : Command {

    protected override void Execute() {
        GameObject levelObject = LevelEditorSelectedLevelObjectStatus.LevelObject;
        LevelObjectTransformType levelObjectTransformType = GenerateableLevelObjectLibrary.GetNode(levelObject.name).GetDefaultLevelObjectInputType();
        LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType = levelObjectTransformType;
    }

}
