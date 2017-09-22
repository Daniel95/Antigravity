using IoCPlus;

public class AbortIfBoxOverlayIsShownCommand : Command {

    protected override void Execute() {
        if(BoxOverlay.Instance.ShowBoxOverlay) {
            Abort();
        }
    }

}
