using IoCPlus;

public class LevelEditorClearSelectionFieldCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    protected override void Execute() {
        selectionFieldStatus.PreviousSelectionFieldGridPositions.Clear();
        selectionFieldStatus.SelectionFieldGridPositions.Clear();
    }

}
