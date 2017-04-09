using IoCPlus;

public class RemoveViewOnPlayerCommand<T> : Command where T : View {

    [Inject] IContext context;

    [Inject] private PlayerModel playerModel;

    protected override void Execute() {
        View view = playerModel.player.GetComponent<T>();

        view.Destroy();
    }
}
