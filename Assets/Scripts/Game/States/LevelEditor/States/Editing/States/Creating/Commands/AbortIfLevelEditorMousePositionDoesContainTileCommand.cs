using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorMousePositionDoesContainTileCommand : Command {

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(Input.mousePosition);
        if (LevelEditorTileGrid.Instance.ContainsTile(gridPosition)) {
            Abort();
        }
    }

}
