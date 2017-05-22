using IoCPlus;

public class AbortIfRevivedStateIsReadyForLaunchInputIsFalseCommand : Command {

    [Inject] private Ref<IRevivedState> revivedStateRef;

    protected override void Execute() {
        if(!revivedStateRef.Get().IsReadyForLaunchInput) {
            Abort();
        }
    }
}
