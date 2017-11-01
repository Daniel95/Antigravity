using IoCPlus;

public class AbortIfPlayerIsRotatingAroundCornerCommand : Command {

    protected override void Execute() {
        if(PlayerRotatingAroundCornerStatusView.Rotating) {
            Abort();
        }
    }

}
