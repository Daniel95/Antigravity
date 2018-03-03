using IoCPlus;
using UnityEngine;

public class AbortIfMousePositionContainsTileCommand : Command {

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(Input.mousePosition);
        if (TileGrid.Instance.ContainsTile(gridPosition)) {
            Abort();
        }
    }

}
