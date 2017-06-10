using IoCPlus;

public class AbortIfMainMenuUIStateIsNotMainMenuStateCommand : Command<MainMenuUIState> {

    [InjectParameter] private MainMenuUIState mainMenuState;

    protected override void Execute(MainMenuUIState mainMenuState) {
        if(this.mainMenuState != mainMenuState) {
            Abort();
        }
    }
}
