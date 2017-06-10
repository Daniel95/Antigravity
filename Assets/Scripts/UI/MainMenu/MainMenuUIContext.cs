using IoCPlus;

public class MainMenuUIContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToMainMenuUIStateEvent>();

        On<EnterContextSignal>()
            .GotoState<StartMenuUIContext>();

        OnChild<ControlsMenuUIContext, GoToMainMenuUIStateEvent>()
            .Do<AbortIfMainMenuUIStateIsNotMainMenuStateCommand>(MainMenuUIState.StartMenu)
            .GotoState<StartMenuUIContext>();

        OnChild<StartMenuUIContext, GoToMainMenuUIStateEvent>()
            .Do<AbortIfMainMenuUIStateIsNotMainMenuStateCommand>(MainMenuUIState.ControlsMenu)
            .GotoState<ControlsMenuUIContext>();
    }

}