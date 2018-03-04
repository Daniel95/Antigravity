﻿using IoCPlus;
using UnityEngine;

public class UpdateSelectionFieldToSwipePositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        Vector2 gridStartDivision = VectorHelper.Divide(SelectionFieldStatusView.SelectionFieldStartGridPosition, GridSnapSizeStatusView.Size);
        Vector2 snapStartPosition = VectorHelper.Floor(gridStartDivision);
        Vector2 startPosition = VectorHelper.Multiply(snapStartPosition, GridSnapSizeStatusView.Size);

        Vector2 gridEndDivision = VectorHelper.Divide(gridPosition, GridSnapSizeStatusView.Size);
        Vector2 snapEndPosition = VectorHelper.Floor(gridEndDivision);
        Vector2 endPosition = VectorHelper.Multiply(snapEndPosition, GridSnapSizeStatusView.Size);

        Vector2 direction = VectorHelper.Clamp(snapEndPosition - snapStartPosition, -1, 1);

        int xOffset = (int)GridSnapSizeStatusView.Size.x - 1;
        int yOffset = (int)GridSnapSizeStatusView.Size.y - 1;

        if (direction.x >= 0) {
            endPosition.x += xOffset;
        } else {
            startPosition.x += yOffset;
        }

        if (direction.y >= 0) {
            endPosition.y += xOffset;
        } else {
            startPosition.y += yOffset;
        }

        SelectionFieldStatusView.SelectionFieldStartGridPosition = startPosition;
        SelectionFieldStatusView.SelectionFieldEndGridPosition = endPosition;
        SelectionFieldStatusView.PreviousSelectionFieldGridPositions = SelectionFieldStatusView.SelectionFieldGridPositions;
        SelectionFieldStatusView.SelectionFieldGridPositions = GridHelper.GetSelection(SelectionFieldStatusView.SelectionFieldStartGridPosition, SelectionFieldStatusView.SelectionFieldEndGridPosition);
    }

}
