using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateTranslateStartOffsetPositionCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateStartOffsetStatus levelobjectTranslateStartOffsetStatus;

    protected override void Execute() {
        Vector2 offset = (Vector2)LevelEditorSelectedLevelObjectStatus.LevelObject.transform.position - LevelEditorTranslateStartPositionStatus.StartWorldPosition;
        levelobjectTranslateStartOffsetStatus.StartOffset = offset;
    }

}
