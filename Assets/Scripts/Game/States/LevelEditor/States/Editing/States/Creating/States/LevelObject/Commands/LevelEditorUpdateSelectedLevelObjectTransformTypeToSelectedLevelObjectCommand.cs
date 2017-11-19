using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectRef.Get().LevelObjectType;
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);

        if (LevelEditorSelectedLevelObjectTransformTypeStatus.TransformType == null) {
            UpdateTransformTypeToLevelObjectDefault(generateableLevelObjectNode);
            return;
        } else {
            bool selectedLevelObjectContainsCurrentTransformType = generateableLevelObjectNode.TransformTypes.Contains((LevelObjectTransformType)LevelEditorSelectedLevelObjectTransformTypeStatus.TransformType);
            if (!selectedLevelObjectContainsCurrentTransformType) {
                UpdateTransformTypeToLevelObjectDefault(generateableLevelObjectNode);
            }
        }
    }

    private void UpdateTransformTypeToLevelObjectDefault(GenerateableLevelObjectNode generateableLevelObjectNode) {
        LevelObjectTransformType defaultTransformType = generateableLevelObjectNode.GetDefaultLevelObjectInputType();
        LevelEditorSelectedLevelObjectTransformTypeStatus.TransformType = defaultTransformType;
    }

}
