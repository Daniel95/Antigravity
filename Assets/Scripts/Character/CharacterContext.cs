﻿using IoCPlus;

public class CharacterContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<CharacterSetSavedDirectionToStartDirectionCommand>()
            .Do<CharacterActivateDirectionalMovementCommand>();

        On<JumpInputEvent>()
            .Do<CharacterTryJumpCommand>();

        On<CharacterRetryJumpEvent>()
            .Do<CharacterRetryJumpCommand>();

        On<CharacterJumpEvent>()
            .Do<CharacterJumpCommand>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>();

        On<CharacterBounceEvent>()
            .Do<CharacterBounceCommand>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>();

        On<CharacterRemoveCollisionDirectionEvent>()
            .Do<CharacterRemoveCollisionDirectionCommand>();

        On<CharacterResetCollisionDirectionEvent>()
            .Do<CharacterResetCollisionDirectionCommand>();

        On<CharacterTurnToNextDirectionEvent>()
            .Do<CharacterTurnToNextDirectionCommand>()
            .Do<CharacterPointToSavedDirectionCommand>();

        On<CharacterUpdateLineDestinationEvent>()
            .Do<CharacterUpdateAimLineDestinationCommand>();

        On<CharacterStopAimLineEvent>()
            .Do<CharacterStopAimLineCommand>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfNotCollidingAndNotInTriggerKillerTagsCommand>();

        On<CharacterPointToDirectionEvent>()
            .Do<CharacterPointToDirectionCommand>();

        On<CharacterSetMoveDirectionEvent>()
            .Do<CharacterSetMoveDirectionCommand>();

        On<CharacterSetVelocityEvent>()
            .Do<CharacterSetVelocityCommand>();
    }
}