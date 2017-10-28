using IoCPlus;

public class AbortIfPlayerJumpStatusIsNotEnabledCommand : Command {

    [Inject] private PlayerJumpStatus playerJumpStatus;

    protected override void Execute() {
        if(!playerJumpStatus.Enabled) {
            Abort();
        }
    }

}