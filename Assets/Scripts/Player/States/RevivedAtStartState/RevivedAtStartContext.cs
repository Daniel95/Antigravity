using IoCPlus;

public class RevivedAtStartContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.RevivedAtStart)
            .Do<PlayerEnableDirectionalMovementCommand>(false)
            .Do<PlayerSetPositionToStartPositionCommand>()
            .Do<PlayerResetVelocityCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>()
            .Do<PlayerPointToMoveDirectionCommand>()
            .Do<WaitForSecondsCommand>(1f)
            .Do<PlayerEnableDirectionalMovementCommand>(true);
    }
}