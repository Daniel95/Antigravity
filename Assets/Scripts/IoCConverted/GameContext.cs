﻿using IoCPlus;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerModel>();

        Bind<StartMoveTowardsEvent>();
        Bind<StopMoveTowardsEvent>();
        Bind<ReachedMoveTowardsDestinationEvent>();

        On<EnterContextSignal>()
            .AddContext<UIContext>()
            .AddContext<PlayerContext>()
            .AddContext<LevelContext>()
            .AddContext<CameraContext>();

        On<ReloadSceneEvent>()
            .Do<ReloadSceneCommand>();
    }

}