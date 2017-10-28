﻿using IoCPlus;

public class PlayerStartAtCheckpointContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerState.RespawnAtCheckpoint)
            .Do<PlayerResetVelocityCommand>()
            .Do<PlayerSetDirectionalMovementCommand>(false)
            .Do<EnableInputCommand>(false)
            .Do<EnableWeaponCommand>(false)
            .Do<EnablePlayerJumpStatusCommand>(false)
            .Do<PlayerResetCollisionsCommand>()
            .Do<PlayerMoveTowardsCheckpointCommand>();

        On<PlayerMoveTowardsCheckpointCompletedEvent>()
            .Do<PlayerSetDirectionalMovementCommand>(false)
            .Do<EnableInputCommand>(true);

        On<DraggingInputEvent>()
            .Do<PlayerUpdateAimLineDestinationCommand>()
            .Do<PlayerPointToDirectionCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<PlayerSetDirectionalMovementCommand>(true)
            .Do<EnableWeaponCommand>(true)
            .Do<EnablePlayerJumpStatusCommand>(true)
            .Do<PlayerStopAimLineCommand>()
            .Do<SetSavedDirectionToCeilVelocityDirectionCommand>()
            .Do<PlayerSetMoveDirectionCommand>()
            .Do<PlayerPointToDirectionCommand>()
            .Do<PlayerTemporarySpeedIncreaseCommand>();
    }
}