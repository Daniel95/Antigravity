using IoCPlus;
using System.Collections.Generic;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<SceneState>();
        Bind<PlayerStatus>();

        Bind<Ref<ICanvasUI>>();
        Bind<Ref<IScreenShake>>();
        Bind<Ref<IFollowCamera>>();

        Bind<GoToSceneEvent>();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabCommand>("UI/Canvas/CanvasUI")
            .Do<InstantiateViewInCanvasLayerCommand>("UI/FPSCounterUI", CanvasLayer.UI)
            .Do<AddCameraViewsCommand>()
            .Do<DispatchLoadSceneCommand>(Scenes.MainMenu);

        On<GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsScenesCommand>(new List<Scenes>() { Scenes.MainMenu, Scenes.LevelSelect })
            .GotoState<LevelContext>();

        On<GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.MainMenu)
            .GotoState<MainMenuUIContext>();

        On<GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.LevelSelect)
            .GotoState<LevelSelectContext>();

        On<ReloadSceneEvent>()
            .Do<ReloadSceneCommand>();

        On<GoToSceneEvent>()
            .Do<AbortIfSceneIsSceneCommand>(Scenes.Main)
            .Do<UnloadCurrentSceneCommand>()
            .Do<LoadSceneCommand>()
            .OnAbort<LoadSceneCommand>();
    }

}