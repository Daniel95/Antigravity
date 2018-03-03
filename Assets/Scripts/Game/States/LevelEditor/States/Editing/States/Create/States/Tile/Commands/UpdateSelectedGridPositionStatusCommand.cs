using IoCPlus;
using UnityEngine;

public class UpdateSelectedGridPositionStatusCommand : Command {

    [Inject] private SelectedGridPositionStatus levelEditorSelectedGridPositionStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        levelEditorSelectedGridPositionStatus.GridPosition = gridPosition;
    }

}
