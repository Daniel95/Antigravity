using IoCPlus;
using UnityEngine;

public class LevelEditorClearSelectionFieldCommand : Command {

    protected override void Execute() {
        LevelEditorSelectionFieldStatusView.PreviousSelectionFieldGridPositions.Clear();
        LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions.Clear();
        LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition = Vector2.zero;
        LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition = Vector2.zero;
    }

}