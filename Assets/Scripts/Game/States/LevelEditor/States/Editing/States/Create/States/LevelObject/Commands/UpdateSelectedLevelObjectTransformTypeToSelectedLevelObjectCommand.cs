using IoCPlus;
using UnityEngine;

public class UpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectRef.Get().LevelObjectType;
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);

        if (SelectedLevelObjectTransformTypeStatusView.TransformType == null) {
            SelectedLevelObjectTransformTypeStatusView.TransformType = generateableLevelObjectNode.GetDefaultLevelObjectInputType();
        } else {
            bool selectedLevelObjectContainsCurrentTransformType = generateableLevelObjectNode.TransformTypes.Contains((LevelObjectTransformType)SelectedLevelObjectTransformTypeStatusView.TransformType);
            if (!selectedLevelObjectContainsCurrentTransformType) {
                SelectedLevelObjectTransformTypeStatusView.TransformType = generateableLevelObjectNode.GetDefaultLevelObjectInputType();
            }
        }
    }

}
