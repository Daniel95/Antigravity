using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateOffGridTranslateStartOffsetPositionCommand : Command {

    [Inject] private LevelEditorOffGridTranslateStartOffsetStatus offGridTranslateStartOffsetStatus;

    protected override void Execute() {
        Vector2 offset = (Vector2)LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject.transform.position - LevelEditorTranslateStartPositionStatus.StartWorldPosition;
        offGridTranslateStartOffsetStatus.StartOffset = offset;
    }

}
