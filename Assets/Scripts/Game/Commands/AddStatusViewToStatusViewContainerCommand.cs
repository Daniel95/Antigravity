using IoCPlus;

public class AddStatusViewToStatusViewContainerCommand<T> : Command where T : View {

    [Inject] private Ref<IStatusViewContainer> statusViewContainerRef;

    protected override void Execute() {
        statusViewContainerRef.Get().AddStatusView<T>();
    }

}
