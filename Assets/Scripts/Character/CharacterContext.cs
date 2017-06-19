using IoCPlus;

public class CharacterContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerActivateDirectionalMovementCommand>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<PlayerPointToVelocityDirectionCommand>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsNotZeroCommand>()
            .Do<WaitForPlayerRetryJumpFramesCommand>()
            .Do<AbortIfPlayerCollisionDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<PlayerPointToVelocityDirectionCommand>();

        On<PlayerBounceEvent>()
            .Do<PlayerBounceCommand>()
            .Do<PlayerPointToCeiledVelocityDirectionCommand>();

        On<PlayerRemoveCollisionDirectionEvent>()
            .Do<PlayerRemoveCollisionDirectionCommand>();

        On<PlayerTurnToNextDirectionEvent>()
            .Do<PlayerTurnToNextDirectionCommand>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<CharacterPointToDirectionEvent>()
            .Do<CharacterPointToDirectionCommand>();

        On<PlayerSetMoveDirectionEvent>()
            .Do<PlayerSetMoveDirectionCommand>();

        On<CollisionEnter2DEvent>()
            .Do<PlayerUpdateCollisionDirectionCommand>();

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