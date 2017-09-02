using IoCPlus;

public class AbortIfGridOverlayIsNotShownCommand : Command {

    protected override void Execute() {
        if(!GridOverlay.Instance.ShowGridOverlay) {
            Abort();
        }
    }

}
