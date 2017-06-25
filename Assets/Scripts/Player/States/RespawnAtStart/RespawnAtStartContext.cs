using IoCPlus;

public class RespawnAtStartContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.RespawnAtStart)
            .Do<PlayerEnableTrailCommand>(false)
            .Do<PlayerSetPositionToStartPositionCommand>()
            .Do<PlayerResetVelocityCommand>()
            .Do<PlayerEnableDirectionalMovementCommand>(false)
            .Do<PlayerEnableTrailCommand>(true)
            .Do<PlayerResetCollisionDirectionCommand>()
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>()
            .Do<PlayerPointToMoveDirectionCommand>()
            .Do<WaitForSecondsCommand>(1f)
            .Do<PlayerEnableDirectionalMovementCommand>(true)
            .Dispatch<PlayerRespawnAtStartCompletedEvent>();

    }
}