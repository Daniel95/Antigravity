using IoCPlus;

public class AbortIfLastLevelIsZeroCommand : Command {

    [Inject] private LevelStatus levelStatus;

    protected override void Execute() {
        if(levelStatus.LastLevelNumber == 0) {
            Abort();
        }
    }

}
