using IoCPlus;

public class AbortIfRevivedStateIsInPositionIsFalseCommand : Command {

    [Inject] private Ref<IRevivedState> revivedStateRef;

    protected override void Execute() {
        if(!revivedStateRef.Get().IsInPosition) {
            Abort();
        }
    }
}
