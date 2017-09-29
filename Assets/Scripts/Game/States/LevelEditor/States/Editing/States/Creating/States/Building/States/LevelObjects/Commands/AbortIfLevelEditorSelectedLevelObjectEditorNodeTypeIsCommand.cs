using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectEditorNodeTypeIsCommand : Command<LevelObjectType> {

    [Inject] private LevelEditorSelectedLevelObjectNodeTypeStatus selectedLevelObjectStatus;

    protected override void Execute(LevelObjectType levelObjectType) {
        if(selectedLevelObjectStatus.LevelObjectType == levelObjectType) {
            Abort();
        }
    }

}
