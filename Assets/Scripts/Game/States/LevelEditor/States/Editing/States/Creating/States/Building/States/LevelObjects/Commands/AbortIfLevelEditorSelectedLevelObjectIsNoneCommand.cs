using IoCPlus;
using UnityEngine;

public class AbortIfLevelEditorSelectedLevelObjectIsNoneCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectStatus selectedLevelObjectStatus;

    protected override void Execute() {
        if(selectedLevelObjectStatus.levelObjectType == LevelObjectType.None) {
            Abort();
        }
    }

}
