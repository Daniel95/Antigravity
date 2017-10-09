using IoCPlus;

public class AbortIfMultiTouchedAfterIdleCommand : Command {

    protected override void Execute() {
        if(TouchInputView.MultiTouchedAfterIdle) {
            Abort();
        }
    }

}
