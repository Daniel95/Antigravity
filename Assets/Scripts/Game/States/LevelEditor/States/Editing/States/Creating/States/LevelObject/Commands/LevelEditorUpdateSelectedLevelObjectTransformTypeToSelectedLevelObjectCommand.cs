using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        GameObject levelObject = selectedLevelObjectRef.Get().GameObject;
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObject.name);

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
