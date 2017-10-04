using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectInputTypeIsNotInputTypeCommand : Command<LevelObjectInputType> {

    protected override void Execute(LevelObjectInputType levelObjectInputType) {
        if(LevelEditorSelectedLevelObjectInputTypeStatus.LevelObjectInputType != levelObjectInputType) {
            Abort();
        }
    }

}
