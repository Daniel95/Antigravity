using IoCPlus;

public class LevelEditorUpdateSelectedLevelObjectInputTypeStatusCommand : Command {

    [InjectParameter] private LevelObjectInputType levelEditorLevelObjectInputType;

    protected override void Execute() {
        LevelEditorSelectedLevelObjectInputTypeStatus.LevelObjectInputType = levelEditorLevelObjectInputType;
    }

}
