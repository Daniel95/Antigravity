using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedOffGridLevelObjectCommand : Command {

    protected override void Execute() {
        GameObject offGridLevelObject = LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject;
        LevelObjectTransformType levelObjectTransformType = GenerateableLevelObjectLibrary.GetNode(offGridLevelObject.name).GetDefaultLevelObjectInputType();
        LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType = levelObjectTransformType;
    }

}
