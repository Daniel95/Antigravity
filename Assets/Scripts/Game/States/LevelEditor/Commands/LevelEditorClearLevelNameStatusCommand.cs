using IoCPlus;

public class LevelEditorClearLevelNameStatusCommand : Command {

    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        levelNameStatus.Name = "";
    }

}
