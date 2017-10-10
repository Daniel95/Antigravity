using IoCPlus;

public class AbortIfSwipeMoving2FingersCommand : Command {

    [Inject] private Ref<ITouchInput> touchInput;

    protected override void Execute() {
        if(touchInput.Get().SwipeMoving2Fingers) {
            Abort();
        }
    }

}
