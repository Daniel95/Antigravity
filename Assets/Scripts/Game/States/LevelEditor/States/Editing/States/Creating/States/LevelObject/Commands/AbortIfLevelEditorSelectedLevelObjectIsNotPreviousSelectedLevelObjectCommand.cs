using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectIsPreviousSelectedLevelObjectCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectStatus.LevelObject == LevelEditorSelectedLevelObjectStatus.PreviousLevelObject) {
            Abort();
        }
    }

}
