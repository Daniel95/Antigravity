using IoCPlus;
using UnityEngine;

public class UpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectRef.Get().LevelObjectType;
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);

        if (SelectedLevelObjectTransformTypeStatus.TransformType == null) {
            UpdateTransformTypeToLevelObjectDefault(generateableLevelObjectNode);
            return;
        } else {
            bool selectedLevelObjectContainsCurrentTransformType = generateableLevelObjectNode.TransformTypes.Contains((LevelObjectTransformType)SelectedLevelObjectTransformTypeStatus.TransformType);
            if (!selectedLevelObjectContainsCurrentTransformType) {
                UpdateTransformTypeToLevelObjectDefault(generateableLevelObjectNode);
            }
        }
    }

    private void UpdateTransformTypeToLevelObjectDefault(GenerateableLevelObjectNode generateableLevelObjectNode) {
        LevelObjectTransformType defaultTransformType = generateableLevelObjectNode.GetDefaultLevelObjectInputType();
        SelectedLevelObjectTransformTypeStatus.TransformType = defaultTransformType;
    }

}
