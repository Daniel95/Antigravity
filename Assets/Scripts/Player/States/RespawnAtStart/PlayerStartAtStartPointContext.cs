using IoCPlus;

public class PlayerStartAtStartPointContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.RespawnAtStart)
            .Do<PlayerSetPositionToStartPointPositionCommand>()
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>()
            .Do<PlayerPointToMoveDirectionCommand>()
            .Do<WaitForSecondsCommand>(1f)
            .Do<PlayerSetDirectionalMovementCommand>(true)
            .Dispatch<PlayerStartAtStartPointCompletedEvent>();

    }
}