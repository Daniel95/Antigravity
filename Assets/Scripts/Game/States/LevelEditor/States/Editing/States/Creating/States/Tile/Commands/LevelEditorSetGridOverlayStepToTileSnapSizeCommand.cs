using IoCPlus;
using UnityEngine;

public class LevelEditorSetGridOverlayStepToTileSnapSizeCommand : Command {

    protected override void Execute() {
        Vector2 tileSize = new Vector2(LevelEditorGridNodeSize.Instance.NodeSize, LevelEditorGridNodeSize.Instance.NodeSize);
        Vector2 tileSnapSize = VectorHelper.Multiply(LevelEditorSelectionFieldSnapSizeStatus.Size, tileSize);
        GridOverlay.Instance.Step = tileSnapSize;
    }

}
