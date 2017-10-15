using IoCPlus;
using UnityEngine;

public class LevelEditorMoveSelectedOffGridLevelObjectToWorldPositionCommand : Command {

    [Inject] private LevelEditorOffGridTranslateStartOffsetStatus offGridTranslateStartOffsetStatus;

    [InjectParameter] private Vector2 worldPosition;

    protected override void Execute() {
        LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject.transform.position = worldPosition + offGridTranslateStartOffsetStatus.StartOffset;
    }

}
