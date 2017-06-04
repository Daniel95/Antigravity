using IoCPlus;

public class RemoveViewOnPlayerCommand<T> : Command where T : View {

    [Inject] IContext context;

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        View view = playerStatus.Player.GetComponent<T>();

        view.Destroy();
    }
}
