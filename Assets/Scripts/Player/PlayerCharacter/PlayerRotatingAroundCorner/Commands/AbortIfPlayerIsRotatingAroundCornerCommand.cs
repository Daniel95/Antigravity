using IoCPlus;

public class AbortIfPlayerIsRotatingAroundCornerCommand : Command {

    protected override void Execute() {
        if(PlayerRotateAroundCornerStatusView.Rotating) {
            Abort();
        }
    }

}
