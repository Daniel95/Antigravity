using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorGridPositionDoesContainElementCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        if (LevelEditorLevelObjectSectionGrid.Instance.Contains(gridPosition)) {
            Abort();
        }
    }

}
