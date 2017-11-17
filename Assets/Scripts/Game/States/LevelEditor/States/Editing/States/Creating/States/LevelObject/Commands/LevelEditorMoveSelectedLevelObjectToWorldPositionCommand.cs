using IoCPlus;
using UnityEngine;

public class LevelEditorMoveSelectedLevelObjectToWorldPositionCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateStartOffsetStatus levelObjectTranslateStartOffsetStatus;

    [InjectParameter] private LevelEditorSwipeMovedOnWorldEvent.Parameter levelEditorSwipeMovedOnWorldEventParameter;

    protected override void Execute() {
        Vector2 worldPosition = levelEditorSwipeMovedOnWorldEventParameter.Position;
        Vector2 startOffset = levelObjectTranslateStartOffsetStatus.StartOffset;
        LevelEditorSelectedLevelObjectStatus.LevelObject.transform.position = worldPosition + startOffset;
    }

}
