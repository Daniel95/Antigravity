using IoCPlus;
using UnityEngine;

public class SetGridOverlayStartToTileSizeCommand : Command {

    protected override void Execute() {
        Vector2 tileSize = TileGenerator.Instance.TileSize;
        GridOverlay.Instance.Start = tileSize;
    }

}
