using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<CharacterTurnToNextDirectionEvent>();
        Bind<CharacterSetMoveDirectionEvent>();
        Bind<CharacterBoostSpeedEvent>();
        Bind<CharacterTemporarySpeedChangeEvent>();
        Bind<CharacterTemporarySpeedDecreaseEvent>();
        Bind<CharacterTemporarySpeedChangeEvent>();
        Bind<CharacterRemoveCollisionDirectionEvent>();
        Bind<CharacterJumpEvent>();
        Bind<CharacterRetryJumpEvent>();
        Bind<CharacterBounceEvent>();
        Bind<CharacterPointToDirectionEvent>();
        Bind<CharacterSetVelocityEvent>();
        Bind<CancelDragInputEvent>();
        Bind<DraggingInputEvent>();
        Bind<HoldingInputEvent>();
        Bind<JumpInputEvent>();
        Bind<ReleaseInputEvent>();
        Bind<ReleaseInDirectionInputEvent>();
        Bind<TappedExpiredInputEvent>();
        Bind<EnableActionInputEvent>();
        Bind<EnableShootingInputEvent>();
        Bind<PlayerCollisionEnter2DEvent>();
        Bind<PlayerCollisionStay2DEvent>();
        Bind<PlayerCollisionExit2DEvent>();
        Bind<PlayerTriggerEnter2DEvent>();
        Bind<PlayerTriggerStay2DEvent>();
        Bind<PlayerTriggerExit2DEvent>();
        Bind<UpdateGrapplingStateEvent>();
        Bind<RawCancelDragInputEvent>();
        Bind<RawDraggingInputEvent>();
        Bind<RawHoldingInputEvent>();
        Bind<RawJumpInputEvent>();
        Bind<RawReleaseInDirectionInputEvent>();
        Bind<RawReleaseInputEvent>();
        Bind<RawTappedExpiredInputEvent>();
        Bind<FireWeaponEvent>();
        Bind<AimWeaponEvent>();
        Bind<CancelAimWeaponEvent>();
        Bind<UpdateHookEvent>();
        Bind<CancelHookEvent>();
        Bind<HookProjectileMoveTowardsOwnerCompletedEvent>();
        Bind<HookProjectileMoveTowardsNextAnchorEvent>();
        Bind<HoldShotEvent>();
        Bind<EnterGrapplingHookContextEvent>();
        Bind<UpdateGrapplingHookRopeEvent>();
        Bind<EnterPullingHookContextSignal>();

        Bind<CollisionEnter2DEvent>();
        Bind<CollisionStay2DEvent>();
        Bind<CollisionExit2DEvent>();
        Bind<TriggerEnter2DEvent>();
        Bind<TriggerStay2DEvent>();
        Bind<TriggerExit2DEvent>();

        Bind<GoToNextSceneEvent>();
        Bind<GoToSceneEvent>();
        Bind<GoToCurrentSceneEvent>();
        Bind<GoToSceneCompletedEvent>();

        Bind<Ref<ICanvasUI>>();
        Bind<Ref<ICamera>>();
        Bind<Ref<ISelectableLevelField>>();
        Bind<Ref<ICamera>>();
        Bind<Ref<IScreenShake>>();
        Bind<Ref<IFollowCamera>>();
        Bind<Ref<IWeapon>>();
        Bind<Ref<IHook>>();
        Bind<Ref<IHookProjectile>>();
        Bind<Ref<IPCInput>>();
        Bind<Ref<IMobileInput>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<ICharacterVelocity>>();
        Bind<Ref<ICharacterTurnDirection>>();
        Bind<Ref<ICharacterRaycastDirection>>();
        Bind<Ref<ICharacterCollisionDirection>>();
        Bind<Ref<ICharacterSurroundingDirection>>();
        Bind<Ref<ICharacterJump>>();
        Bind<Ref<ICharacterDirectionPointer>>();
        Bind<Ref<ICharacterAimLine>>();
        Bind<Ref<ICharacterBounce>>();
        Bind<Ref<ICharacterDie>>();
        Bind<Ref<ICharacterSpeed>>();
        Bind<Ref<ICollisionHitDetection>>();
        Bind<Ref<ITriggerHitDetection>>();
        Bind<Ref<ISlowTime>>();
        Bind<Ref<IGrapplingState>>();
        Bind<Ref<IRevivedState>>();
        BindLabeled<Ref<IMoveTowards>>(Label.Player);

        Bind<IGameStateService, LocalGameStateService>();

        Bind<Ref<GameStateModel>>();

        Bind<LevelStatus>();
        Bind<SceneStatus>();
        Bind<ViewContainerStatus>();
        Bind<InputStatus>();

        On<EnterContextSignal>()
            .Do<LoadGameStateCommand>()
            .Do<InstantiateViewPrefabCommand>("UI/Canvas/CanvasUI")
            .Do<InstantiateViewInCanvasLayerCommand>("UI/FPSCounterUI", CanvasLayer.UI)
            .Do<AddCameraViewsCommand>()
            .Do<SetNextSceneToIndicatedSceneCommand>(Scenes.MainMenu)
            .GotoState<LoadingContext>();

        OnChild<LoadingContext, GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsScenesCommand>(new List<Scenes>() { Scenes.MainMenu, Scenes.LevelSelect })
            .GotoState<LevelContext>();

        OnChild<LoadingContext, GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.MainMenu)
            .GotoState<MainMenuUIContext>();

        OnChild<LoadingContext, GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.LevelSelect)
            .GotoState<LevelSelectContext>();

        On<GoToCurrentSceneEvent>()
            .Do<SetNextSceneToCurrentSceneCommand>()
            .GotoState<LoadingContext>();

        On<GoToNextSceneEvent>()
            .Do<SetNextSceneToNextSceneCommand>()
            .GotoState<LoadingContext>();

        On<GoToSceneEvent>()
            .Do<SetNextSceneToSceneCommand>()
            .GotoState<LoadingContext>();

        On<ApplicationPauseEvent>()
            .Do<SaveGameStateCommand>();

        if (Application.isEditor) {
            On<ApplicationQuitEvent>()
                .Do<SaveGameStateCommand>();
        }

    }

}