using IoCPlus;
using UnityEngine;

public class LevelEditorMoveSelectedLevelObjectToGridPositionCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectSectionStatus levelEditorSelectedLevelObjectStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelObjectSection levelObjectSection = levelEditorSelectedLevelObjectStatus.LevelObjectSection;
        Vector2 levelObjectSectionGridPosition = levelEditorSelectedLevelObjectStatus.GridPosition;
        Vector2 offset = gridPosition - levelObjectSectionGridPosition;

        levelObjectSection.IncrementLevelObjectGridPosition(offset);
    }

}
