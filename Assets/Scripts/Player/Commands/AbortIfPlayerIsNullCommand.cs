using IoCPlus;

public class AbortIfPlayerIsNullCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        if(playerStatus.Player == null) {
            Abort();
        }
    }

}
