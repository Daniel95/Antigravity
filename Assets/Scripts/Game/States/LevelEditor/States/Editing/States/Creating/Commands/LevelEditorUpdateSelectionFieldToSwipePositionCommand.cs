using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectionFieldToSwipePositionCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        Vector2 selectionFieldEndWorldScreenPosition = Camera.main.ScreenToWorldPoint(swipeMoveEventParameter.Position);

        float nodeSize = LevelEditorGridNodeSize.Instance.Size;
        selectionFieldStatus.SelectionFieldEndGridPosition = GridHelper.WorldToGridPosition(selectionFieldEndWorldScreenPosition, nodeSize);

        selectionFieldStatus.PreviousSelectionFieldGridPositions = selectionFieldStatus.SelectionFieldGridPositions;
        selectionFieldStatus.SelectionFieldGridPositions = GridHelper.GetSelection(selectionFieldStatus.SelectionFieldStartGridPosition, selectionFieldStatus.SelectionFieldEndGridPosition);
    }

}
