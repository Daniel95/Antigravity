using IoCPlus;
using UnityEngine;

public class SetGridOverlayStepToTileSizeCommand : Command {

    protected override void Execute() {
        Vector2 tileSize = new Vector2(LevelEditorGridNodeSize.Instance.NodeSize, LevelEditorGridNodeSize.Instance.NodeSize);
        GridOverlay.Instance.Step = tileSize;
    }

}
