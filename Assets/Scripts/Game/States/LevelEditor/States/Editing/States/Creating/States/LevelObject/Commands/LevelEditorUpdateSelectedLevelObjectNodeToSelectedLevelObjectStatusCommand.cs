using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectNodeToSelectedLevelObjectStatusCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectRef.Get().LevelObjectType;
        LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
    }


}
