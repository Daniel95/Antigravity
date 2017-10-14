using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectSectionCommand : Command {

    protected override void Execute() {
        LevelObjectType levelObjectType = LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection.LevelObject.LevelObjectType;
        LevelObjectTransformType levelObjectTransformType = GenerateableLevelObjectLibrary.GetNode(levelObjectType).GetDefaultLevelObjectInputType();
        LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType = levelObjectTransformType;
    }

}
