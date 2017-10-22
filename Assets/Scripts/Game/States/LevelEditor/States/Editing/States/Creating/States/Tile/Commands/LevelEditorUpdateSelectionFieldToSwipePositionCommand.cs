﻿using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectionFieldToSwipePositionCommand : Command {

    [Inject] private LevelEditorSelectionFieldSnapSizeStatus selectionFieldSnapSizeStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        Vector2 gridStartDivision = VectorHelper.Divide(LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition, selectionFieldSnapSizeStatus.Size);
        Vector2 snapStartPosition = VectorHelper.Floor(gridStartDivision);
        Vector2 startPosition = VectorHelper.Multiply(snapStartPosition, selectionFieldSnapSizeStatus.Size);

        Vector2 gridEndDivision = VectorHelper.Divide(gridPosition, selectionFieldSnapSizeStatus.Size);
        Vector2 snapEndPosition = VectorHelper.Floor(gridEndDivision);
        Vector2 endPosition = VectorHelper.Multiply(snapEndPosition, selectionFieldSnapSizeStatus.Size);

        Vector2 direction = VectorHelper.Clamp(snapEndPosition - snapStartPosition, -1, 1);

        int xOffset = (int)selectionFieldSnapSizeStatus.Size.x - 1;
        int yOffset = (int)selectionFieldSnapSizeStatus.Size.y - 1;

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

        LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition = startPosition;
        LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition = endPosition;
        LevelEditorSelectionFieldStatusView.PreviousSelectionFieldGridPositions = LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions;
        LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions = GridHelper.GetSelection(LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition, LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition);
    }

}
