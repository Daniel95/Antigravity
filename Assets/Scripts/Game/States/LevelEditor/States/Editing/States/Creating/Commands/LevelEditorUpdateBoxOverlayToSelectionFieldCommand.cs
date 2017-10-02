using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateBoxOverlayToSelectionFieldCommand : Command {

    [InjectParameter] private LevelEditorSelectionFieldChangedEvent.Parameter parameter;

    protected override void Execute() {
        Vector2 halfTileSize = new Vector2(LevelEditorGridNodeSize.Instance.NodeSize / 2, LevelEditorGridNodeSize.Instance.NodeSize / 2);

        float minX = Mathf.Min(parameter.SelectionFieldStartPosition.x, parameter.SelectionFieldEndPosition.x);
        float minY = Mathf.Min(parameter.SelectionFieldStartPosition.y, parameter.SelectionFieldEndPosition.y);
        float maxX = Mathf.Max(parameter.SelectionFieldStartPosition.x, parameter.SelectionFieldEndPosition.x);
        float maxY = Mathf.Max(parameter.SelectionFieldStartPosition.y, parameter.SelectionFieldEndPosition.y);

        Vector2 bottomLeftCornerGridPosition = new Vector2(minX, minY);
        Vector2 topRightCornerGridPosition = new Vector2(maxX, maxY);

        Vector2 bottomLeftCornerWorldPosition = LevelEditorGridHelper.GridToNodePosition(bottomLeftCornerGridPosition) - halfTileSize;
        Vector2 topRightCornerWorldPosition = LevelEditorGridHelper.GridToNodePosition(topRightCornerGridPosition) + halfTileSize;

        BoxOverlay.Instance.UpdateBox(bottomLeftCornerWorldPosition, topRightCornerWorldPosition);
    }

}
