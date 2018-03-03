using IoCPlus;
using UnityEngine;

public class AbortIfGridPositionDoesNotContainTileCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        if (!TileGrid.Instance.ContainsTile(gridPosition)) {
            Abort();
        }
    }

}
