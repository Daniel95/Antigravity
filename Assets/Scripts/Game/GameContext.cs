using IoCPlus;
using System.Collections.Generic;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GameStateModel>();
        Bind<PlayerStatus>();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabCommand>("Views/UI/CanvasUI")
            .AddContext<UIContext>();

        On<LoadSceneEvent>()
            .Do<AbortIfSceneIsScenesCommand>(new List<Scenes>() { Scenes.MainMenu, Scenes.LevelSelect })
            .GotoState<LevelContext>();

        On<LoadSceneEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.MainMenu)
            .GotoState<MainMenuUIContext>();

        On<LoadSceneEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.LevelSelect)
            .GotoState<LevelSelectContext>();

        On<ReloadSceneEvent>()
            .Do<ReloadSceneCommand>();

        On<LoadSceneEvent>()
            .Do<LoadSceneCommand>();
    }

}