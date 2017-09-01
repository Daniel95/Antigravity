using IoCPlus;
using UnityEngine;

public class SetGridOverlayStepToTileSizeCommand : Command {

    protected override void Execute() {
        Vector2 tileSize = TileGenerator.Instance.TileSize;
        GridOverlay.Instance.Step = tileSize;
    }

}
