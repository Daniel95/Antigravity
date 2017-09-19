using IoCPlus;

public class LevelEditorUpdateLevelNameStatusCommand : Command {

    [Inject] private LevelNameStatus levelNameStatus;

    [InjectParameter] private string newLevelName;

    protected override void Execute() {
        levelNameStatus.Name = newLevelName;
    }

}
