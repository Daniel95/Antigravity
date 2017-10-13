using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorGridPositionDoesContainLevelObjectSectionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        if(LevelEditorLevelObjectSectionGrid.Instance.ContainsLevelObjectSection(gridPosition)) {
            Abort();
        }
    }

}