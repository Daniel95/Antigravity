using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectionFieldToSwipePositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition = gridPosition;
        Debug.Log("Update endposition to " + gridPosition);
        Debug.Log("startposition is now " + LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition);
        LevelEditorSelectionFieldStatusView.PreviousSelectionFieldGridPositions = LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions;
        LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions = GridHelper.GetSelection(LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition, LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition);
    }

}
