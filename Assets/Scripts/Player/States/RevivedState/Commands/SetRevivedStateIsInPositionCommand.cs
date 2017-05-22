using IoCPlus;

public class SetRevivedStateIsInPositionCommand : Command<bool> {

    [Inject] private Ref<IRevivedState> revivedStateRef;

    protected override void Execute(bool value) {
        revivedStateRef.Get().IsInPosition = value;
    }
}
