using IoCPlus;

public class AbortIfPlayerIsNotRotatingAroundCornerCommand : Command {

    protected override void Execute() {
        if(!PlayerRotateAroundCornerStatusView.Rotating) {
            Abort();
        }
    }

}
