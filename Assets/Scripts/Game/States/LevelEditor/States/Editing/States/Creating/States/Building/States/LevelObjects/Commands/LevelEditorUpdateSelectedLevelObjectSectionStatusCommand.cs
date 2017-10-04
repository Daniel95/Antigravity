using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectSectionStatusCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelObjectSection levelObjectSection = LevelEditorLevelObjectSectionGrid.Instance.GetLevelObjectSection(gridPosition);
        LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection = levelObjectSection;
    }

}
