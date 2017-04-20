using IoCPlus;

public class SetCheckpointReachedCommand : Command<bool> {

    [Inject] private PlayerModel playerModel;

    protected override void Execute(bool state) {
        playerModel.checkpointReached = state;
    }
}
