using IoCPlus;

public class AbortIfPlayerJumpIsNotEnabledCommand : Command {

    [Inject] private PlayerJumpStatus playerJumpStatus;

    protected override void Execute() {
        if(!playerJumpStatus.JumpIsEnabled) {
            Abort();
        }
    }
}