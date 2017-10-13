using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorMousePositionDoesContainLevelObjectSectionCommand : Command {

    [InjectParameter] private Vector2 mousePosition;

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(mousePosition);
        if (LevelEditorLevelObjectSectionGrid.Instance.ContainsLevelObjectSection(gridPosition)) {
            Abort();
        }
    }

}