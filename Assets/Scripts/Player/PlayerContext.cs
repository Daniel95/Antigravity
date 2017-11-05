﻿using IoCPlus;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .Do<PlayerSetPositionToStartPointPositionCommand>()
            .AddContext<InputContext>()
            .AddContext<WeaponContext>()
            .AddContext<PlayerStateContext>()
            .Do<AddStatusViewToStatusViewContainerCommand<PlayerRotateAroundCornerStatusView>>()
            .Do<EnableInputCommand>(true)
            .Do<EnableWeaponCommand>(true)
            .Do<EnablePlayerJumpStatusCommand>(true)
            .Do<PlayerEnableTurnStatusCommand>(true)
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>()
            .Do<PlayerPointToMoveDirectionCommand>();

        On<EnterContextSignal>()
            .Do<AbortIfPlayerSessionStatsStatusLevelDeathsIsZeroCommand>()
            .Do<AbortIfReachedCheckPointIsNullCommand>()
            .Dispatch<PlayerStartAtCheckpointEvent>();

        On<EnterContextSignal>()
            .Do<AbortIfPlayerSessionStatsStatusLevelDeathsIsZeroCommand>()
            .Do<AbortIfReachedCheckPointIsNotNullCommand>()
            .Dispatch<PlayerStartAtStartPointEvent>();

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<PlayerRotateAroundCornerStatusView>>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.Finish)
            .Do<AddCurrentSceneToCompletedLevelsCommand>()
            .Do<SaveGameStateCommand>()
            .Dispatch<GoToNextSceneEvent>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerJumpStatusIsNotEnabledCommand>()
            .Dispatch<PlayerTryJumpEvent>();

        On<PlayerTryJumpEvent>()
            .Do<PlayerCancelRotatingAroundCornerCommand>();

        On<PlayerTryJumpEvent>()
            .Do<AbortIfPlayerSurroundingDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Dispatch<PlayerJumpedEvent>()
            .OnAbort<DispatchPlayerJumpFailedEventCommand>();

        On<PlayerJumpFailedEvent>()
            .Do<AbortIfPlayerSurroundingDirectionIsZeroCommand>()
            .Do<WaitForPlayerRetryJumpTimeCommand>()
            .Do<AbortIfPlayerSurroundingDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Dispatch<PlayerJumpedEvent>();

        On<PlayerJumpedEvent>()
            .Do<PlayerPointToVelocityDirectionCommand>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfPlayerNotCollidingAndNotInTriggerWithTagCommand>(Tags.Bouncy)
            .Do<DispatchPlayerBounceEventCommand>();

        On<PlayerBounceEvent>()
            .Do<AbortIfPlayerBounceEventParameterCollisionDirectionIsZeroCommand>()
            .Do<PlayerBounceCommand>()
            .Do<PlayerPointToCeiledVelocityDirectionCommand>();

        On<PlayerResetSavedCollisionsEvent>()
            .Do<PlayerResetCollisionsCommand>();

        On<PlayerTurnToNextDirectionEvent>()
            .Do<AbortIfPlayerTurnStatusIsNotEnabledCommand>()
            .Do<PlayerTurnToNextDirectionCommand>()
            .Do<PlayerPointToSavedDirectionCommand>();

        On<CharacterSetMoveDirectionEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerSetMoveDirectionEventCommand>();

        On<PlayerSetMoveDirectionEvent>()
            .Do<PlayerSetMoveDirectionCommand>();

        On<PlayerStartedRotatingAroundCornerEvent>()
            .Do<PlayerEnableDirectionalMovementCommand>(false)
            .Do<PlayerResetVelocityCommand>()
            .Do<PlayerEnableTurnStatusCommand>(false);

        On<PlayerStoppedRotatingAroundCornerEvent>()
            .Do<PlayerEnableDirectionalMovementCommand>(true)
            .Do<PlayerEnableTurnStatusCommand>(true);

        On<PlayerDiedEvent>()
            .Dispatch<CancelDragInputEvent>()
            .Do<HookProjectileStopMoveTowardsCommand>()
            .Do<PlayerStopAllCheckingRotateAroundCornerConditionsCommand>()
            .Do<PlayerCancelRotatingAroundCornerCommand>()
            .Do<ShakeInOutCommand>(ShakeType.PlayerDied)
            .Do<InstantiatePrefabOnPlayerPositionCommand>("Effects/DieEffect");

        On<PlayerDiedEvent>()
            .Dispatch<PlayerResetSavedCollisionsEvent>()
            .Do<DestroyPlayerCommand>()
            .Dispatch<PlayerRespawnEvent>();

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