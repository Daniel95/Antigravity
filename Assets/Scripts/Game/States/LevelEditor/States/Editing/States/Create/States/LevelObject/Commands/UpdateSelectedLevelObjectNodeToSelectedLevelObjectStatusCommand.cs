using IoCPlus;
using UnityEngine;

public class UpdateSelectedLevelObjectNodeToSelectedLevelObjectStatusCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectRef.Get().LevelObjectType;
        SelectedLevelObjectNodeStatus.LevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
    }

}
