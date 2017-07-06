using IoCPlus;

public class PlayerRespawnAtStartContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.RespawnAtStart)
            .Do<PlayerSetPositionToStartPositionCommand>()
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>()
            .Do<PlayerPointToMoveDirectionCommand>()
            .Do<WaitForSecondsCommand>(1f)
            .Do<PlayerSetDirectionalMovementCommand>(true)
            .Dispatch<PlayerRespawnAtStartCompletedEvent>();

    }
}