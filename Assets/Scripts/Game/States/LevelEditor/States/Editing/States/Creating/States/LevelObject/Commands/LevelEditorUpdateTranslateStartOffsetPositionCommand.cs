using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateTranslateStartOffsetPositionCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;
    [Inject] private LevelEditorLevelObjectTranslateStartOffsetStatus levelobjectTranslateStartOffsetStatus;

    protected override void Execute() {
        Vector2 selectedLevelObjectPosition = selectedLevelObjectRef.Get().GameObject.transform.position;
        Vector2 offset = selectedLevelObjectPosition - LevelEditorTranslateStartPositionStatus.StartWorldPosition;
        levelobjectTranslateStartOffsetStatus.StartOffset = offset;
    }

}
