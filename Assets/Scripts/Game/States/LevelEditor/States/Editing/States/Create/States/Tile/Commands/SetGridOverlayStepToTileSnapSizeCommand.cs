using IoCPlus;
using UnityEngine;

public class SetGridOverlayStepToTileSnapSizeCommand : Command {

    protected override void Execute() {
        Vector2 tileSize = new Vector2(LevelEditorGridNodeSizeLibrary.Instance.NodeSize, LevelEditorGridNodeSizeLibrary.Instance.NodeSize);
        Vector2 tileSnapSize = VectorHelper.Multiply(GridSnapSizeStatusView.Size, tileSize);
        GridOverlay.Instance.Step = tileSnapSize;
    }

}
