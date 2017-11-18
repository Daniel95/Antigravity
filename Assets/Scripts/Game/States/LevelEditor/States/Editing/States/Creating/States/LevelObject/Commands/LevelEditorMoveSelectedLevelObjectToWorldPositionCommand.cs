using IoCPlus;
using UnityEngine;

public class LevelEditorMoveSelectedLevelObjectToWorldPositionCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateStartOffsetStatus levelObjectTranslateStartOffsetStatus;

    [InjectParameter] private LevelEditorSwipeMovedOnWorldEvent.Parameter levelEditorSwipeMovedOnWorldEventParameter;

    protected override void Execute() {
        Vector2 worldPosition = levelEditorSwipeMovedOnWorldEventParameter.Position;
        Vector2 startOffset = levelObjectTranslateStartOffsetStatus.StartOffset;

        LevelEditorSelectedLevelObjectStatus.LevelObjectRigidBody.MovePosition(worldPosition + startOffset);
        LevelEditorSelectedLevelObjectStatus.LevelObjectRigidBody.velocity = Vector2.zero;
    }

}
