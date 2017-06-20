using IoCPlus;
using System.Collections.Generic;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .AddContext<InputContext>()
            .AddContext<WeaponContext>()
            .AddContext<PlayerStateContext>()
            .Do<EnableInputCommand>(true)
            .Do<EnableWeaponCommand>(true)
            .Do<EnablePlayerJumpCommand>(true)
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerActivateDirectionalMovementCommand>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.Finish)
            .Do<AddCurrentSceneToCompletedLevelsCommand>()
            .Do<SaveGameStateCommand>()
            .Dispatch<GoToNextSceneEvent>();

        On<RespawnPlayerEvent>()
            .Do<InstantiatePlayerCommand>()
            .Do<StartScreenShakeCommand>();

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

        On<PlayerSetMoveDirectionEvent>()
            .Do<PlayerSetMoveDirectionCommand>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<PlayerUpdateCollisionDirectionCommand>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfPlayerHittingTriggerTagsDoesNotContainPlayerKillerTagsCommand>()
            .Do<GameObjectDestroyViewCommand>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfPlayerCollidingTagIsCharacterKillerTagCommand>()
            .Do<GameObjectDestroyViewCommand>()
            .Dispatch<RespawnPlayerEvent>();

        On<PlayerCollisionStay2DEvent>()
            .Do<AbortIfPlayerHittingTriggerTagsDoesNotContainPlayerKillerTagsCommand>()
            .Do<GameObjectDestroyViewCommand>();

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