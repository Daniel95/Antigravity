using IoCPlus;
using UnityEngine;

public class LevelEditorStartSelectionFieldAtScreenPositionCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 selectionFieldStartWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        float nodeSize = LevelEditorGridNodeSize.Instance.NodeSize;

        selectionFieldStatus.SelectionFieldStartGridPosition = GridHelper.WorldToGridPosition(selectionFieldStartWorldPosition, nodeSize);
        selectionFieldStatus.SelectionFieldEndGridPosition = selectionFieldStatus.SelectionFieldStartGridPosition;

        selectionFieldStatus.PreviousSelectionFieldGridPositions = selectionFieldStatus.SelectionFieldGridPositions;
        selectionFieldStatus.SelectionFieldGridPositions = GridHelper.GetSelection(selectionFieldStatus.SelectionFieldStartGridPosition, selectionFieldStatus.SelectionFieldEndGridPosition);
    }

}
