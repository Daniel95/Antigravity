using IoCPlus;
using UnityEngine;

public class InstantiatePlayerViewCommand<T> : Command where T : View {

    [Inject] IContext context;

    [Inject] private PlayerModel playerModel;

    protected override void Execute() {
        View view = playerModel.player.AddComponent<T>();

        context.InstantiateView(view);
    }
}
