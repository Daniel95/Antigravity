using IoCPlus;

public class StartRevivedStateDelayLaunchInputCommand : Command {

    [Inject] private Ref<IRevivedState> revivedStateRef;

    protected override void Execute() {
        revivedStateRef.Get().StartDelayLaunchInput();
    }
}
