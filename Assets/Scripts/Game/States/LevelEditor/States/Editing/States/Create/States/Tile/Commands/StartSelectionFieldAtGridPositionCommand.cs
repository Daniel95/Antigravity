using IoCPlus;
using UnityEngine;

public class StartSelectionFieldAtGridPositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        Vector2 startPosition = VectorHelper.FloorTo(gridPosition, GridSnapSizeStatusView.Size);
        Vector2 endPosition = startPosition;

        endPosition.x += GridSnapSizeStatusView.Size.x - 1;
        endPosition.y += GridSnapSizeStatusView.Size.y - 1;

        SelectionFieldStatusView.SelectionFieldStartGridPosition = startPosition;
        SelectionFieldStatusView.SelectionFieldEndGridPosition = endPosition;

        SelectionFieldStatusView.PreviousSelectionFieldGridPositions = SelectionFieldStatusView.SelectionFieldGridPositions;
        SelectionFieldStatusView.SelectionFieldGridPositions = GridHelper.GetSelection(SelectionFieldStatusView.SelectionFieldStartGridPosition, SelectionFieldStatusView.SelectionFieldEndGridPosition);
    }

}
