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

        Bind<LoadSceneEvent>();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabCommand>("UI/Canvas/CanvasUI")
            .Do<InstantiateViewInCanvasLayerCommand>("UI/FPSCounterUI", CanvasLayer.UI)
            .Do<AddCameraViewsCommand>()
            .Do<DispatchLoadSceneCommand>(Scenes.MainMenu);

        On<LoadSceneCompletedEvent>()
            .Do<AbortIfSceneIsScenesCommand>(new List<Scenes>() { Scenes.MainMenu, Scenes.LevelSelect })
            .GotoState<LevelContext>();

        On<LoadSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.MainMenu)
            .GotoState<MainMenuUIContext>();

        On<LoadSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.LevelSelect)
            .GotoState<LevelSelectContext>();

        On<ReloadSceneEvent>()
            .Do<ReloadSceneCommand>();

        On<LoadSceneEvent>()
            .Do<LoadSceneCommand>();
    }

}