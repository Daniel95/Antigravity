using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectIsNoneCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectStatus levelEditorSelectedLevelObjectStatus;

    protected override void Execute() {
        if(levelEditorSelectedLevelObjectStatus.levelObjectType == LevelObjectType.None) {
            Abort();
        }
    }

}
