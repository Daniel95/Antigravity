using IoCPlus;

public class AbortIfPinchingCommand : Command {

    protected override void Execute() {
        if(TouchInputView.IsPinching) {
            Abort();
        }
    }

}
