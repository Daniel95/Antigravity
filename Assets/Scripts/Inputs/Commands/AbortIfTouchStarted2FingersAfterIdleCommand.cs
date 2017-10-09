using IoCPlus;

public class AbortIfTouchStarted2FingersAfterIdleCommand : Command {

    [Inject] private Ref<ITouchInput> touchInputRef;

    protected override void Execute() {
        if(touchInputRef.Get().TouchStarted2FingersAfterIdle) {
            Abort();
        }
    }

}
