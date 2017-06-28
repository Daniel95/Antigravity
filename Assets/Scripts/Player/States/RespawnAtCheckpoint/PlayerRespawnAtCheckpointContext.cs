using IoCPlus;

public class PlayerRespawnAtCheckpointContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.RespawnAtCheckpoint)
            .Dispatch<CancelDragInputEvent>()
            .Do<PlayerResetVelocityCommand>()
            .Do<PlayerResetMoveDirectionCommand>()
            .Do<EnableInputCommand>(false)
            .Do<EnableWeaponCommand>(false)
            .Do<EnablePlayerJumpCommand>(false)
            .Do<PlayerResetCollisionDirectionCommand>()
            .Do<PlayerMoveTowardsCheckpointCommand>();

        On<PlayerMoveTowardsCheckpointCompletedEvent>()
            .Do<EnableInputCommand>(true);

        On<DraggingInputEvent>()
            .Do<PlayerUpdateAimLineDestinationCommand>()
            .Do<PlayerPointToDirectionCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<EnableWeaponCommand>(true)
            .Do<EnablePlayerJumpCommand>(true)
            .Do<PlayerStopAimLineCommand>()
            .Do<PlayerSetMoveDirectionCommand>()
            .Do<PlayerTemporarySpeedIncreaseCommand>();
    }
}