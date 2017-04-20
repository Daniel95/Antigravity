using IoCPlus;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerModel>();

        On<EnterContextSignal>()
            .AddContext<PlayerContext>()
            .AddContext<LevelContext>()
            .AddContext<CameraContext>();

        On<ReloadSceneEvent>()
            .Do<ReloadSceneCommand>();
    }

}