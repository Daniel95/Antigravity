using IoCPlus;

public class MainMenuContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToMainMenuStateEvent>();

        On<EnterContextSignal>()
            .GotoState<StartMenuContext>();

        On<GoToMainMenuStateEvent>()
            .Do<AbortIfMainMenuStateIsNotMainMenuStateCommand>(MainMenuState.StartMenu)
            .GotoState<StartMenuContext>();

        On<GoToMainMenuStateEvent>()
            .Do<AbortIfMainMenuStateIsNotMainMenuStateCommand>(MainMenuState.ControlsMenu)
            .GotoState<ControlsMenuContext>();
    }

}