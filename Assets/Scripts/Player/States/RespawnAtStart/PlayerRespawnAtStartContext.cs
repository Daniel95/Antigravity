using IoCPlus;

public class PlayerRespawnAtStartContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.RespawnAtStart)
            .Do<PlayerEnableTrailCommand>(false)
            .Do<PlayerSetPositionToStartPositionCommand>()
            .Do<PlayerEnableDirectionalMovementCommand>(false)
            .Do<PlayerResetVelocityCommand>()
            .Do<WaitFramesCommand>(1)
            .Do<PlayerSetPositionToStartPositionCommand>()
            .Do<PlayerResetVelocityCommand>()
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