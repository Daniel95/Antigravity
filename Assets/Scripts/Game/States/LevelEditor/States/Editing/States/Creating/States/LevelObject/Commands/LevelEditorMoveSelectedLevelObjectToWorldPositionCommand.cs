using IoCPlus;
using UnityEngine;

public class LevelEditorMoveSelectedLevelObjectToWorldPositionCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateStartOffsetStatus levelObjectTranslateStartOffsetStatus;

    [InjectParameter] private Vector2 worldPosition;

    protected override void Execute() {
        LevelEditorSelectedLevelObjectStatus.LevelObject.transform.position = worldPosition + levelObjectTranslateStartOffsetStatus.StartOffset;
    }

}
