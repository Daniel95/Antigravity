﻿using IoCPlus;

public class PlayerGrapplingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Grappling)
            .Do<PlayerSetDirectionalMovementCommand>(false)
            .Do<StartUpdatePlayerGrapplingCommand>();

        On<LeaveContextSignal>()
            .Do<StopUpdatePlayerGrapplingCommand>()
            .Dispatch<CancelHookEvent>();

        On<PlayerTryJumpEvent>()
            .Do<PlayerSetMoveDirectionToVelocityDirectionCommand>()
            .Do<PlayerTemporarySpeedIncreaseCommand>();

        On<UpdatePlayerGrapplingEvent>()
            .Do<PlayerSetVelocityToVelocityDirectionSpeedCommand>();

        On<UpdatePlayerGrapplingEvent>()
            .Do<AbortIfPlayerIsNotStuckCommand>()
            .Do<DispatchPlayerTurnToNextDirectionEventCommand>()
            .Dispatch<PlayerIsStuckEvent>();
    }
}