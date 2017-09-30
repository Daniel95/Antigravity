using IoCPlus;
using UnityEngine;

public class LevelEditorResetSelectedLevelObjectSectionStatusCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectSectionStatus levelEditorSelectedLevelObjectSectionStatus;

    protected override void Execute() {
        levelEditorSelectedLevelObjectSectionStatus.GridPosition = Vector2.zero;
        levelEditorSelectedLevelObjectSectionStatus.LevelObjectSection = null;
    }

}
