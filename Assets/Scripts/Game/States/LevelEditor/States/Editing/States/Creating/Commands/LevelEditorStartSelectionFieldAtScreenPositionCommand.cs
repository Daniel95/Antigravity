using IoCPlus;
using UnityEngine;

public class LevelEditorStartSelectionFieldAtGridPositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition = gridPosition;
        LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition = LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition;

        LevelEditorSelectionFieldStatusView.PreviousSelectionFieldGridPositions = LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions;
        LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions = GridHelper.GetSelection(LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition, LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition);
    }

}
