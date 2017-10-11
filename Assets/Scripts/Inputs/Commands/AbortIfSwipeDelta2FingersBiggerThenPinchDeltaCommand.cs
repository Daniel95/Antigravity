using IoCPlus;

public class AbortIfSwipeDelta2FingersBiggerThenPinchDeltaCommand : Command {

    [Inject] private Ref<ITouchInput> touchInput;

    protected override void Execute() {
        if(touchInput.Get().SwipeDelta2FingersBiggerThenPinchDelta) {
            Abort();
        }
    }

}
