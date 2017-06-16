using IoCPlus;

public class AbortIfJumpIsNotEnabledCommand : Command {

    [Inject] private JumpStatus jumpStatus;

    protected override void Execute() {
        if(!jumpStatus.JumpIsEnabled) {
            Abort();
        }
    }
}