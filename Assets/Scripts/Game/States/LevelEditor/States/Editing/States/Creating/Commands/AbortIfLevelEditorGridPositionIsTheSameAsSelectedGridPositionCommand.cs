using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorGridPositionIsTheSameAsSelectedGridPositionCommand : Command {

    [Inject] private LevelEditorSelectedGridPositionStatus levelEditorSelectedGridPositionStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        if(gridPosition == levelEditorSelectedGridPositionStatus.GridPosition) {
            Abort();
        }
    }

}
