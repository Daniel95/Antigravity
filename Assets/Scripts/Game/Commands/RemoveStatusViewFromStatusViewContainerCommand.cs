using IoCPlus;

public class RemoveStatusViewFromStatusViewContainerCommand<T> : Command where T : StatusView {

    [Inject] private Ref<IStatusViewContainer> statusViewContainerRef;

    protected override void Execute() {
        statusViewContainerRef.Get().RemoveStatusView<T>();
    }

}
