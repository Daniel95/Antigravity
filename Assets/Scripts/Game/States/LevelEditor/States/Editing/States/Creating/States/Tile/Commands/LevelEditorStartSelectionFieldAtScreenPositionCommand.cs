using IoCPlus;
using UnityEngine;

public class LevelEditorStartSelectionFieldAtGridPositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        Vector2 startPosition = VectorHelper.FloorTo(gridPosition, LevelEditorSelectionFieldSnapSizeStatus.Size);
        Vector2 endPosition = startPosition;

        endPosition.x += LevelEditorSelectionFieldSnapSizeStatus.Size.x - 1;
        endPosition.y += LevelEditorSelectionFieldSnapSizeStatus.Size.y - 1;

        LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition = startPosition;
        LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition = endPosition;

        LevelEditorSelectionFieldStatusView.PreviousSelectionFieldGridPositions = LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions;
        LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions = GridHelper.GetSelection(LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition, LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition);
    }

}
