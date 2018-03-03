using IoCPlus;

public class ClearLevelNameStatusCommand : Command {

    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        levelNameStatus.Name = "";
    }

}
