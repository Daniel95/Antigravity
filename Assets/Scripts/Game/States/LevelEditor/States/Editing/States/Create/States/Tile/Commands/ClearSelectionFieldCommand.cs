using IoCPlus;
using UnityEngine;

public class ClearSelectionFieldCommand : Command {

    protected override void Execute() {
        SelectionFieldStatusView.PreviousSelectionFieldGridPositions.Clear();
        SelectionFieldStatusView.SelectionFieldGridPositions.Clear();
        SelectionFieldStatusView.SelectionFieldStartGridPosition = Vector2.zero;
        SelectionFieldStatusView.SelectionFieldEndGridPosition = Vector2.zero;
    }

}