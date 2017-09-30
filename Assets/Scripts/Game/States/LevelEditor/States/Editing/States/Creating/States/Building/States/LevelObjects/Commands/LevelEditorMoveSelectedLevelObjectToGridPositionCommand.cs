using IoCPlus;
using UnityEngine;

public class LevelEditorMoveSelectedLevelObjectToGridPositionCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectSectionStatus levelEditorSelectedLevelObjectStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelObjectSection levelObjectSection = levelEditorSelectedLevelObjectStatus.LevelObjectSection;
        levelObjectSection.SetGridPosition(gridPosition);
    }

}
