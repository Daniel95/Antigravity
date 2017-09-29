using IoCPlus;
using UnityEngine;

public class LevelEditorStartSelectionFieldAtScreenPositionCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 selectionFieldStartWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        selectionFieldStatus.SelectionFieldStartGridPosition = LevelEditorGridHelper.WorldToGridPosition(selectionFieldStartWorldPosition);
        selectionFieldStatus.SelectionFieldEndGridPosition = selectionFieldStatus.SelectionFieldStartGridPosition;

        selectionFieldStatus.PreviousSelectionFieldGridPositions = selectionFieldStatus.SelectionFieldGridPositions;
        selectionFieldStatus.SelectionFieldGridPositions = GridHelper.GetSelection(selectionFieldStatus.SelectionFieldStartGridPosition, selectionFieldStatus.SelectionFieldEndGridPosition);
    }

}
