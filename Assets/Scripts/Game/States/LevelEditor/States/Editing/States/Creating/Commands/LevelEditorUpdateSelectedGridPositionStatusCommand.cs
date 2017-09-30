using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedGridPositionStatusCommand : Command {

    [Inject] private LevelEditorSelectedGridPositionStatus levelEditorSelectedGridPositionStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        levelEditorSelectedGridPositionStatus.GridPosition = gridPosition;
    }

}
