using IoCPlus;
using UnityEngine;

public class LevelEditorStartSelectionFieldAtGridPositionCommand : Command {

    [Inject] private LevelEditorSelectionFieldSnapSizeStatus selectionFieldSnapSizeStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        Vector2 startPosition = VectorHelper.FloorTo(gridPosition, selectionFieldSnapSizeStatus.Size);
        Vector2 endPosition = startPosition;

        endPosition.x += selectionFieldSnapSizeStatus.Size.x - 1;
        endPosition.y += selectionFieldSnapSizeStatus.Size.y - 1;

        LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition = startPosition;
        LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition = endPosition;

        LevelEditorSelectionFieldStatusView.PreviousSelectionFieldGridPositions = LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions;
        LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions = GridHelper.GetSelection(LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition, LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition);
    }

}
