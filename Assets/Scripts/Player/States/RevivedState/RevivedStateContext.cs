﻿using IoCPlus;

public class RevivedStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Revived)
            .Dispatch<CancelDragInputEvent>()
            .Do<CharacterResetVelocityCommand>()
            .Do<CharacterResetMoveDirectionCommand>()
            .Do<EnableInputCommand>(false)
            .Do<EnableWeaponCommand>(false)
            .Do<EnablePlayerJumpCommand>(false)
            .Do<CharacterResetCollisionDirectionCommand>()
            .Do<PlayerMoveTowardsCheckpointCommand>();

        On<PlayerMoveTowardsCheckpointCompletedEvent>()
            .Do<EnableInputCommand>(true);

        On<DraggingInputEvent>()
            .Do<PlayerUpdateAimLineDestinationCommand>()
            .Do<CharacterPointToDirectionCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<EnableWeaponCommand>(true)
            .Do<EnablePlayerJumpCommand>(true)
            .Do<PlayerStopAimLineCommand>()
            .Do<CharacterSetMoveDirectionCommand>()
            .Do<PlayerTemporarySpeedIncreaseCommand>();
    }
}