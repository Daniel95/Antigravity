using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorGridPositionDoesNotContainLevelObjectSectionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        if(!LevelEditorLevelObjectSectionGrid.Instance.ContainsLevelObjectSection(gridPosition)) {
            Abort();
        }
    }

}