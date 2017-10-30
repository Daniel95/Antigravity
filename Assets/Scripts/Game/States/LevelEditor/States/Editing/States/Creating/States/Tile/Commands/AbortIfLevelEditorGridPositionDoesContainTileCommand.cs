using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorGridPositionDoesContainTileCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        if (LevelEditorTileGrid.Instance.ContainsTile(gridPosition)) {
            Abort();
        }
    }

}
