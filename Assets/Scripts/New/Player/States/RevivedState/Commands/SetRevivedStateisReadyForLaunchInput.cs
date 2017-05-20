using IoCPlus;

public class SetRevivedStateIsReadyForLaunchInput : Command<bool> {

    [Inject] private Ref<IRevivedState> revivedStateRef;

    protected override void Execute(bool value) {
        revivedStateRef.Get().IsReadyForLaunchInput = value;
    }
}
