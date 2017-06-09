using IoCPlus;

public class AbortIfMainMenuStateIsNotMainMenuStateCommand : Command<MainMenuState> {

    [InjectParameter] private MainMenuState mainMenuState;

    protected override void Execute(MainMenuState mainMenuState) {
        if(this.mainMenuState != mainMenuState) {
            Abort();
        }
    }
}
