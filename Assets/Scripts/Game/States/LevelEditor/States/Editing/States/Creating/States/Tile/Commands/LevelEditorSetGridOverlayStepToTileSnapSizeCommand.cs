using IoCPlus;
using UnityEngine;

public class LevelEditorSetGridOverlayStepToTileSnapSizeCommand : Command {

    [Inject] private LevelEditorSelectionFieldSnapSizeStatus selectionFieldSnapSizeStatus;

    protected override void Execute() {
        Vector2 tileSize = new Vector2(LevelEditorGridNodeSize.Instance.NodeSize, LevelEditorGridNodeSize.Instance.NodeSize);
        Vector2 tileSnapSize = VectorHelper.Multiply(selectionFieldSnapSizeStatus.Size, tileSize);
        GridOverlay.Instance.Step = tileSnapSize;
    }

}
