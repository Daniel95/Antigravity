using IoCPlus;

public class CharacterContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<CharacterSetSavedDirectionToStartDirectionCommand>()
            .Do<CharacterActivateDirectionalMovementCommand>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<CharacterPointToVelocityDirectionCommand>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsNotZeroCommand>()
            .Do<WaitForPlayerRetryJumpFramesCommand>()
            .Do<AbortIfPlayerCollisionDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<CharacterPointToVelocityDirectionCommand>();

        On<PlayerBounceEvent>()
            .Do<PlayerBounceCommand>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>();

        On<PlayerRemoveCollisionDirectionEvent>()
            .Do<CharacterRemoveCollisionDirectionCommand>();

        On<CharacterResetCollisionDirectionEvent>()
            .Do<CharacterResetCollisionDirectionCommand>();

        On<CharacterTurnToNextDirectionEvent>()
            .Do<CharacterTurnToNextDirectionCommand>()
            .Do<CharacterPointToSavedDirectionCommand>();

        On<CharacterPointToDirectionEvent>()
            .Do<CharacterPointToDirectionCommand>();

        On<PlayerSetMoveDirectionEvent>()
            .Do<CharacterSetMoveDirectionCommand>();

        On<CharacterSetVelocityEvent>()
            .Do<CharacterSetVelocityCommand>();

        On<CollisionEnter2DEvent>()
            .Do<CharacterUpdateCollisionDirectionCommand>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotACharacterCommand>()
            .Do<AbortIfPlayerHittingTriggerTagsDoesNotContainPlayerKillerTagsCommand>()
            .Do<GameObjectDestroyViewCommand>();

        On<CollisionStay2DEvent>()
            .Do<AbortIfGameObjectIsNotACharacterCommand>()
            .Do<AbortIfPlayerHittingTriggerTagsDoesNotContainPlayerKillerTagsCommand>()
            .Do<GameObjectDestroyViewCommand>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotACharacterCommand>()
            .Do<AbortIfPlayerCollidingTagIsCharacterKillerTagCommand>()
            .Do<GameObjectDestroyViewCommand>();
    }
}