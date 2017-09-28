using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectionFieldToSwipePositionCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        selectionFieldStatus.SelectionFieldEndGridPosition = gridPosition;

        selectionFieldStatus.PreviousSelectionFieldGridPositions = selectionFieldStatus.SelectionFieldGridPositions;
        selectionFieldStatus.SelectionFieldGridPositions = GridHelper.GetSelection(selectionFieldStatus.SelectionFieldStartGridPosition, selectionFieldStatus.SelectionFieldEndGridPosition);
    }

}
