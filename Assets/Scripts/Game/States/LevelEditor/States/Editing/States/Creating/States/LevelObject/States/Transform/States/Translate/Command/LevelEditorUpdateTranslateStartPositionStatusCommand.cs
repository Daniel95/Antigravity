using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateTranslateStartPositionStatusCommand : Command {

    [InjectParameter] private Vector2 worldPosition;

    protected override void Execute() {
        LevelEditorTranslateStartPositionStatus.StartWorldPosition = worldPosition;
    }

}
