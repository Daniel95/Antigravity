using IoCPlus;

public class EnablePlayerTurnStatusCommand : Command<bool> {

    [Inject] private PlayerTurnStatus playerTurnStatus;

    protected override void Execute(bool enable) {
        playerTurnStatus.Enabled = enable;
    }

}