﻿using IoCPlus;

public class GrapplingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Grappling)
            .Do<CharacterEnableDirectionalMovementCommand>(false)
            .Do<StartUpdateGrapplingStateCommand>();

        On<LeaveContextSignal>()
            .Do<StopUpdateGrapplingStateCommand>()
            .Dispatch<CancelHookEvent>();

        On<JumpInputEvent>()
            .Do<CharacterSetMoveDirectionToVelocityDirectionCommand>()
            .Do<CharacterTemporarySpeedIncreaseCommand>();

        On<UpdateGrapplingStateEvent>()
            .Do<UpdateGrapplingStateVelocityCommand>();

        On<UpdateGrapplingStateEvent>()
            .Do<AbortIfCharacterIsNotStuckCommand>()
            .Do<DispatchCharacterTurnToNextDirectionEventCommand>()
            .Dispatch<CharacterIsStuckEvent>();
    }
}