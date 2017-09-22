using IoCPlus;
using UnityEngine;

public class SetGridOverlayOriginToHalfTileSizeCommand : Command {

    protected override void Execute() {
        Vector2 tileSize = TileGenerator.Instance.TileSize;
        GridOverlay.Instance.Origin = tileSize / 2;
    }

}
