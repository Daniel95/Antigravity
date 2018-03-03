using IoCPlus;
using UnityEngine;

public class StartSelectionFieldAtGridPositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        Vector2 startPosition = VectorHelper.FloorTo(gridPosition, GridSnapSizeStatus.Size);
        Vector2 endPosition = startPosition;

        endPosition.x += GridSnapSizeStatus.Size.x - 1;
        endPosition.y += GridSnapSizeStatus.Size.y - 1;

        SelectionFieldStatusView.SelectionFieldStartGridPosition = startPosition;
        SelectionFieldStatusView.SelectionFieldEndGridPosition = endPosition;

        SelectionFieldStatusView.PreviousSelectionFieldGridPositions = SelectionFieldStatusView.SelectionFieldGridPositions;
        SelectionFieldStatusView.SelectionFieldGridPositions = GridHelper.GetSelection(SelectionFieldStatusView.SelectionFieldStartGridPosition, SelectionFieldStatusView.SelectionFieldEndGridPosition);
    }

}
