using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateTranslateStartPositionStatusCommand : Command {

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        LevelEditorTranslateStartPositionStatus.StartWorldPosition = worldPosition;
    }

}
