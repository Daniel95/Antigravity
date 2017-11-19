using IoCPlus;
using UnityEngine;

public class LevelEditorDestroySelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    protected override void Execute() {
        levelEditorLevelObjectsStatus.DestroyLevelObject(selectedLevelObjectRef.Get().GameObject);
    }

}
