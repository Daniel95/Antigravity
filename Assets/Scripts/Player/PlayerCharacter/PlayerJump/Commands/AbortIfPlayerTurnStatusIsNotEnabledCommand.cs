using IoCPlus;

public class AbortIfPlayerTurnStatusIsNotEnabledCommand : Command {

    [Inject] private PlayerTurnStatus playerTurnStatus;

    protected override void Execute() {
        if (!playerTurnStatus.Enabled) {
            Abort();
        }
    }

}