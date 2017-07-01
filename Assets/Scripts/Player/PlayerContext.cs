using IoCPlus;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .Do<PlayerSetPositionToStartPositionCommand>()
            .AddContext<InputContext>()
            .AddContext<WeaponContext>()
            .AddContext<PlayerStateContext>()
            .Do<EnableInputCommand>(true)
            .Do<EnableWeaponCommand>(true)
            .Do<EnablePlayerJumpCommand>(true)
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>()
            .Do<PlayerEnableDirectionalMovementCommand>(true);

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.Finish)
            .Do<AddCurrentSceneToCompletedLevelsCommand>()
            .Do<SaveGameStateCommand>()
            .Dispatch<GoToNextSceneEvent>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerSurroundingDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<PlayerPointToVelocityDirectionCommand>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerSurroundingDirectionIsZeroCommand>()
            .Do<WaitForPlayerRetryJumpTimeCommand>()
            .Do<AbortIfPlayerSurroundingDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<PlayerPointToVelocityDirectionCommand>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfPlayerNotCollidingAndNotInTriggerWithTagCommand>(Tags.Bouncy)
            .Do<DispatchPlayerBounceEventCommand>();

        On<PlayerBounceEvent>()
            .Do<AbortIfPlayerBounceEventParameterCollisionDirectionIsZeroCommand>()
            .Do<PlayerBounceCommand>()
            .Do<PlayerPointToCeiledVelocityDirectionCommand>();

        On<PlayerRemoveCollisionDirectionEvent>()
            .Do<PlayerRemoveCollisionDirectionCommand>();

        On<PlayerTurnToNextDirectionEvent>()
            .Do<PlayerTurnToNextDirectionCommand>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<PlayerSetMoveDirectionEvent>()
            .Do<PlayerSetMoveDirectionCommand>();

        On<PlayerDiedEvent>()
            .Do<ShakeInOutCommand>(ShakeType.PlayerDied)
            .Do<InstantiatePrefabOnPlayerPositionCommand>("Effects/DieEffect");

        On<PlayerDiedEvent>()
            .Do<AbortIfReachedCheckPointIsNullCommand>()
            .Dispatch<PlayerRespawnAtCheckpointEvent>();

        On<PlayerDiedEvent>()
            .Do<AbortIfReachedCheckPointIsNotNullCommand>()
            .Dispatch<PlayerRespawnAtStartEvent>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<PlayerUpdateCollisionDirectionCommand>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfPlayerCollidingTagIsPlayerKillerTagCommand>()
            .Dispatch<PlayerDiedEvent>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfPlayerHittingTriggerTagsDoesNotContainPlayerKillerTagsCommand>()
            .Dispatch<PlayerDiedEvent>();

        On<PlayerCollisionStay2DEvent>()
            .Do<AbortIfPlayerHittingTriggerTagsDoesNotContainPlayerKillerTagsCommand>()
            .Dispatch<PlayerDiedEvent>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionEnter2DEvent>();

        On<CollisionStay2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionStay2DEvent>();

        On<CollisionExit2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionExit2DEvent>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerEnter2DEvent>();

        On<TriggerStay2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerStay2DEvent>();

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerExit2DEvent>();
    }
}