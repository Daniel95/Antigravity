using IoCPlus;

public class RemoveStatusViewFromStatusViewContainerCommand<T> : Command where T : View {

    [Inject] private Ref<IStatusViewContainer> statusViewContainerRef;

    protected override void Execute() {
        statusViewContainerRef.Get().RemoveStatusView<T>();
    }

}
