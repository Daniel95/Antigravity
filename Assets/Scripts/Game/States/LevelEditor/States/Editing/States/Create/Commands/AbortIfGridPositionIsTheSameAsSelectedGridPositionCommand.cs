using IoCPlus;
using UnityEngine;

public class AbortIfGridPositionIsTheSameAsSelectedGridPositionCommand : Command {

    [Inject] private SelectedGridPositionStatus selectedGridPositionStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        if(gridPosition == selectedGridPositionStatus.GridPosition) {
            Abort();
        }
    }

}
