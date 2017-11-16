using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateTranslateStartOffsetPositionCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateStartOffsetStatus levelobjectTranslateStartOffsetStatus;

    protected override void Execute() {
        Vector2 selectedLevelObjectPosition = LevelEditorSelectedLevelObjectStatus.LevelObject.transform.position;
        Vector2 offset = selectedLevelObjectPosition - LevelEditorTranslateStartPositionStatus.StartWorldPosition;
        levelobjectTranslateStartOffsetStatus.StartOffset = offset;
    }

}
