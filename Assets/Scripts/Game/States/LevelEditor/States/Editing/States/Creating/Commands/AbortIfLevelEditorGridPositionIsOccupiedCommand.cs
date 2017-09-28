using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorGridPositionIsOccupiedCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        if (LevelEditorLevelObjectGrid.Instance.Contains(gridPosition)) {
            Abort();
        }
    }

}
