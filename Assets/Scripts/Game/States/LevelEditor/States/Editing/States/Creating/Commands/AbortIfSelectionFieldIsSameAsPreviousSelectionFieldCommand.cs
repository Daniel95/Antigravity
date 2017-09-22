using IoCPlus;
using ExtraListExtensions;
using UnityEngine;

public class AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    protected override void Execute() {
        if(selectionFieldStatus.SelectionFieldGridPositions.Matches(selectionFieldStatus.PreviousSelectionFieldGridPositions)) {
            Abort();
        }
    }

}
