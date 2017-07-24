using IoCPlus;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .Do<PlayerSetPositionToStartPointPositionCommand>()
            .AddContext<InputContext>()
            .AddContext<WeaponContext>()
            .AddContext<PlayerStateContext>()
            .Do<EnableInputCommand>(true)
            .Do<EnableWeaponCommand>(true)
            .Do<EnablePlayerJumpCommand>(true)
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>();

        On<EnterContextSignal>()
            .Do<AbortIfPlayerSessionStatsStatusLevelDeathsIsZeroCommand>()
            .Do<AbortIfReachedCheckPointIsNullCommand>()
            .Dispatch<PlayerStartAtCheckpointEvent>();

        On<EnterContextSignal>()
            .Do<AbortIfPlayerSessionStatsStatusLevelDeathsIsZeroCommand>()
            .Do<AbortIfReachedCheckPointIsNotNullCommand>()
            .Dispatch<PlayerStartAtStartPointEvent>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.Finish)
            .Do<AddCurrentSceneToCompletedLevelsCommand>()
            .Do<SaveGameStateCommand>()
            .Dispatch<GoToNextSceneEvent>();

        On<JumpInputEvent>()
            .Dispatch<PlayerTryJumpEvent>();

        On<PlayerTryJumpEvent>()
            .Do<AbortIfPlayerSurroundingDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<PlayerPointToVelocityDirectionCommand>();

        On<PlayerTryJumpEvent>()
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

        On<CharacterSetMoveDirectionEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerSetMoveDirectionEventCommand>();

        On<PlayerSetMoveDirectionEvent>()
            .Do<PlayerSetMoveDirectionCommand>();

        On<PlayerDiedEvent>()
            .Dispatch<CancelDragInputEvent>()
            .Do<ShakeInOutCommand>(ShakeType.PlayerDied)
            .Do<InstantiatePrefabOnPlayerPositionCommand>("Effects/DieEffect");

        On<PlayerDiedEvent>()
            .Do<PlayerResetCollisionDirectionCommand>()
            .Do<DestroyPlayerCommand>()
            .Dispatch<PlayerRespawnEvent>();

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

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.JumpTrigger)
            .Dispatch<PlayerTryJumpEvent>();

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