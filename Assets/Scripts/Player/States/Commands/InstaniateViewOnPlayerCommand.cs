using IoCPlus;

public class InstantiateViewOnPlayerCommand<T> : Command where T : View {

    [Inject] IContext context;

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        View view = playerStatus.Player.AddComponent<T>();

        context.InstantiateView(view);
    }
}
