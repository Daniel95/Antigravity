using IoCPlus;
using ExtraListExtensions;
using UnityEngine;

public class AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand : Command {

    protected override void Execute() {
        if(SelectionFieldStatusView.SelectionFieldGridPositions.Matches(SelectionFieldStatusView.PreviousSelectionFieldGridPositions)) {
            Abort();
        }
    }

}
