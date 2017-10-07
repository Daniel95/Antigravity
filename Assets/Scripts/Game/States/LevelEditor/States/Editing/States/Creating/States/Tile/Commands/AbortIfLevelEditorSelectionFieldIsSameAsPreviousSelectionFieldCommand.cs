using IoCPlus;
using ExtraListExtensions;
using UnityEngine;

public class AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions.Matches(LevelEditorSelectionFieldStatusView.PreviousSelectionFieldGridPositions)) {
            Abort();
        }
    }

}
