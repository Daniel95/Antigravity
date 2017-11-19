using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand : Command {

    protected override void Execute() {
        GameObject levelObject = LevelEditorSelectedLevelObjectStatus.LevelObject;
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObject.name);
        bool selectedLevelObjectContainsCurrentTransformType = generateableLevelObjectNode.TransformTypes.Contains(LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType);
        if (!selectedLevelObjectContainsCurrentTransformType) {
            LevelObjectTransformType defaultTransformType = generateableLevelObjectNode.GetDefaultLevelObjectInputType();
            LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType = defaultTransformType;
        } 
    }

}
