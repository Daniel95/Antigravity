using IoCPlus;

public class AbortIfPlayerIsNotRotatingAroundCornerCommand : Command {

    protected override void Execute() {
        if(!PlayerRotatingAroundCornerStatusView.Rotating) {
            Abort();
        }
    }

}
