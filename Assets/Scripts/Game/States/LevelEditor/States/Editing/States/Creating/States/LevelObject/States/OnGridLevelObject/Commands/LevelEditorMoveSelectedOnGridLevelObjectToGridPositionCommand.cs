using IoCPlus;
using UnityEngine;

public class LevelEditorMoveSelectedOnGridLevelObjectToGridPositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelObjectSection levelObjectSection = LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection;
        levelObjectSection.SetLevelObjectGridPosition(gridPosition);
    }

}
