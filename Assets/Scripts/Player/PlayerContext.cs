using IoCPlus;
using System.Collections.Generic;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .Do<SetPlayerPositionToStartPositionCommand>()
            .AddContext<InputContext>()
            .AddContext<WeaponContext>()
            .AddContext<PlayerStateContext>()
            .Do<EnableInputCommand>(true)
            .Do<EnableWeaponCommand>(true)
            .Do<EnablePlayerJumpCommand>(true)
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>()
            .Do<PlayerActivateDirectionalMovementCommand>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.Finish)
            .Do<AddCurrentSceneToCompletedLevelsCommand>()
            .Do<SaveGameStateCommand>()
            .Dispatch<GoToNextSceneEvent>();

        On<PlayerDiedEvent>()
            .Do<InstantiatePrefabOnPlayerPositionCommand>("Effects/DieEffect");

        On<PlayerDiedEvent>()
            .Do<AbortIfReachedCheckPointIsNullCommand>()
            .Dispatch<PlayerRespawnAtCheckpointEvent>();

        On<PlayerDiedEvent>()
            .Do<AbortIfReachedCheckPointIsNotNullCommand>()
            .Do<SetPlayerPositionToStartPositionCommand>()
            .Do<PlayerSetMoveDirectionToStartDirectionCommand>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<PlayerPointToVelocityDirectionCommand>();

        On<JumpInputEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsNotZeroCommand>()
            .Do<WaitForPlayerRetryJumpTimeCommand>()
            .Do<AbortIfPlayerCollisionDirectionIsZeroCommand>()
            .Do<PlayerJumpCommand>()
            .Do<PlayerPointToVelocityDirectionCommand>();

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