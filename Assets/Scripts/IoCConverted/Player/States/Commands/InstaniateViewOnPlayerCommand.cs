﻿using IoCPlus;

public class InstantiateViewOnPlayerCommand<T> : Command where T : View {

    [Inject] IContext context;

    [Inject] private PlayerModel playerModel;

    protected override void Execute() {
        View view = playerModel.Player.AddComponent<T>();

        context.InstantiateView(view);
    }
}
