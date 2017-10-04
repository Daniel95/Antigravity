using IoCPlus;
using UnityEngine;

public class LevelEditorMoveSelectedLevelObjectToGridPositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelObjectSection levelObjectSection = LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection;
        levelObjectSection.SetLevelObjectGridPosition(gridPosition);
    }

}
