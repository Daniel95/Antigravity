using IoCPlus;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<Ref<IHook>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<IHookProjectile>>();

        Bind<Ref<IPCInput>>();

        Bind<PlayerModel>();

        Bind<StartMoveTowardsEvent>();
        Bind<StopMoveTowardsEvent>();

        On<EnterContextSignal>()
            .AddContext<PlayerContext>()
            .AddContext<LevelContext>()
            .AddContext<CameraContext>();

        On<ReloadSceneEvent>()
            .Do<ReloadSceneCommand>();
    }

}