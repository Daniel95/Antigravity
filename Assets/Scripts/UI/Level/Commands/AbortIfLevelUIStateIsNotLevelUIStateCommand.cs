using IoCPlus;

public class AbortIfLevelUIStateIsNotLevelUIStateCommand : Command<LevelUIState> {

    [InjectParameter] private LevelUIState levelUIState;

    protected override void Execute(LevelUIState levelUIState) {
        if(this.levelUIState != levelUIState) {
            Abort();
        }
    }
}
