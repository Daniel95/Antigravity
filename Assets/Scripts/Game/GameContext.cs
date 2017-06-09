using IoCPlus;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GameStateModel>();
        Bind<PlayerStatus>();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabCommand>("Views/UI/CanvasUI")
            .AddContext<UIContext>()
            .AddContext<PlayerContext>()
            .AddContext<LevelContext>()
            .AddContext<CameraContext>();

        On<ReloadSceneEvent>()
            .Do<ReloadSceneCommand>();
    }

}