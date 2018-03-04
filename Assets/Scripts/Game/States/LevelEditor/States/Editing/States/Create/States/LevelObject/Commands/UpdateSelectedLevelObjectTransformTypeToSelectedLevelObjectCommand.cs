using IoCPlus;
using UnityEngine;

public class UpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectRef.Get().LevelObjectType;
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);

        if (SelectedLevelObjectTransformTypeStatus.TransformType == null) {
            SelectedLevelObjectTransformTypeStatus.TransformType = generateableLevelObjectNode.GetDefaultLevelObjectInputType();
        } else {
            bool selectedLevelObjectContainsCurrentTransformType = generateableLevelObjectNode.TransformTypes.Contains((LevelObjectTransformType)SelectedLevelObjectTransformTypeStatus.TransformType);
            if (!selectedLevelObjectContainsCurrentTransformType) {
                SelectedLevelObjectTransformTypeStatus.TransformType = generateableLevelObjectNode.GetDefaultLevelObjectInputType();
            }
        }
    }

}
