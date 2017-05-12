﻿using IoCPlus;
using UnityEngine;

public class CharacterContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<ActivateViewOnPlayerCommand<CharacterVelocityView>>()
            .Do<CharacterSetSavedDirectionToStartDirectionCommand>()
            .Do<CharacterActivateDirectionalMovementCommand>();

        On<JumpInputEvent>()
            .Do<CharacterTryJumpCommand>();

        On<CharacterRetryJumpEvent>()
            .Do<CharacterRetryJumpCommand>();

        On<CharacterJumpEvent>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<CharacterJumpCommand>();

        On<CharacterBounceEvent>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<CharacterBounceCommand>();

        On<CharacterRemoveCollisionDirectionEvent>()
            .Do<CharacterRemoveCollisionDirectionCommand>();

        On<CharacterResetCollisionDirectionEvent>()
            .Do<CharacterResetCollisionDirectionCommand>();

        On<CharacterTurnToNextDirectionEvent>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<CharacterTurnToNextDirectionCommand>();

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