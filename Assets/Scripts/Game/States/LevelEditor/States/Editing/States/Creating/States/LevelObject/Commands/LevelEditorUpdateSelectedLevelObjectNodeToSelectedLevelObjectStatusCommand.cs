using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectNodeToSelectedLevelObjectStatusCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        GameObject selectedLevelObject = selectedLevelObjectRef.Get().GameObject;
        LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode = GenerateableLevelObjectLibrary.GetNode(selectedLevelObject.name);
    }


}
