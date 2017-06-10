using IoCPlus;

public class MainMenuContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToMainMenuStateEvent>();

        On<EnterContextSignal>()
            .GotoState<StartMenuContext>();

        OnChild<ControlsMenuContext, GoToMainMenuStateEvent>()
            .Do<AbortIfMainMenuStateIsNotMainMenuStateCommand>(MainMenuState.StartMenu)
            .GotoState<StartMenuContext>();

        OnChild<StartMenuContext, GoToMainMenuStateEvent>()
            .Do<AbortIfMainMenuStateIsNotMainMenuStateCommand>(MainMenuState.ControlsMenu)
            .GotoState<ControlsMenuContext>();
    }

}