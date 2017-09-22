using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectionFieldToSwipePositionCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        Vector2 selectionFieldEndWorldScreenPosition = Camera.main.ScreenToWorldPoint(swipeMoveEventParameter.Position);
        selectionFieldStatus.SelectionFieldEndGridPosition = TileGrid.WorldToGridPosition(selectionFieldEndWorldScreenPosition);

        selectionFieldStatus.PreviousSelectionFieldGridPositions = selectionFieldStatus.SelectionFieldGridPositions;
        selectionFieldStatus.SelectionFieldGridPositions = TileGrid.GetSelection(selectionFieldStatus.SelectionFieldStartGridPosition, selectionFieldStatus.SelectionFieldEndGridPosition);
    }

}
