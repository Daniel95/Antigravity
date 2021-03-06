﻿using IoCPlus;
using UnityEngine;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerTurnToNextDirectionEvent>();
        Bind<CharacterSetMoveDirectionEvent>();
        Bind<PlayerBoostSpeedEvent>();
        Bind<PlayerTemporarySpeedChangeEvent>();
        Bind<PlayerTemporarySpeedDecreaseEvent>();
        Bind<PlayerTemporarySpeedChangeEvent>();
        Bind<PlayerTryJumpEvent>();
        Bind<PlayerJumpFailedEvent>();
        Bind<PlayerJumpedEvent>();
        Bind<PlayerBounceEvent>();
        Bind<PlayerRespawnEvent>();
        Bind<PlayerStartAtCheckpointEvent>();
        Bind<PlayerStartAtStartPointEvent>();
        Bind<PlayerStartAtStartPointCompletedEvent>();
        Bind<PlayerStartedRotatingAroundCornerEvent>();
        Bind<PlayerStoppedRotatingAroundCornerEvent>();
        Bind<CancelDragInputEvent>();
        Bind<DraggingInputEvent>();
        Bind<HoldingInputEvent>();
        Bind<JumpInputEvent>();
        Bind<ReleaseInputEvent>();
        Bind<ReleaseInDirectionInputEvent>();
        Bind<TappedExpiredInputEvent>();
        Bind<PlayerCollisionEnter2DEvent>();
        Bind<PlayerCollisionStay2DEvent>();
        Bind<PlayerCollisionExit2DEvent>();
        Bind<PlayerTriggerEnter2DEvent>();
        Bind<PlayerTriggerStay2DEvent>();
        Bind<PlayerTriggerExit2DEvent>();
        Bind<UpdatePlayerGrapplingEvent>();
        Bind<RawCancelDragInputEvent>();
        Bind<RawDraggingInputEvent>();
        Bind<RawHoldingInputEvent>();
        Bind<RawTapInputEvent>();
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
        Bind<EnterPullingHookContextEvent>();
        Bind<CameraZoomedEvent>();
        Bind<LevelNameInputFieldValueChangedEvent>();
        Bind<SaveCreatedLevelButtonClickedEvent>();
        Bind<LevelSelectButtonClickedEvent>();
        Bind<SetLevelSelectButtonInteractableEvent>();
        Bind<SaveCreatedLevelEvent>();
        Bind<SelectionFieldChangedEvent>();
        Bind<SelectionFieldTileSpawnLimitReachedEvent>();
        Bind<TouchStartOnGridPositionEvent>();
        Bind<TouchUpOnGridPositionEvent>();
        Bind<LevelEditorSwipeMovedToGridPositionEvent>();
        Bind<SwipeMovedOnWorldEvent>();
        Bind<SwipeStartOnWorldEvent>();
        Bind<TouchDownOnWorldEvent>();
        Bind<TouchStartOnWorldEvent>();
        Bind<TouchUpOnWorldEvent>();
        Bind<SpawningTilesStoppedEvent>();
        Bind<LevelObjectButtonClickedEvent>();
        Bind<SelectionFieldEnabledUpdatedEvent>();
        Bind<SwipeMovedToNewGridPositionEvent>();
        Bind<LevelObjectTransformTypeButtonClickedEvent>();
        Bind<LevelObjectDeleteButtonClickedEvent>();
        Bind<SelectedLevelObjectSectionStatusUpdatedEvent>();
        Bind<SelectedLevelObjectTransformTypeStatusUpdatedEvent>();
        Bind<LoadLevelSaveDataCommand>();
        Bind<GridSnapSizeStatusUpdatedEvent>();
        Bind<TouchDownOnLevelObjectEvent>();
        Bind<TouchDownOnTileEvent>();
        Bind<LevelObjectCollisionEnter2DEvent>();
        Bind<LevelObjectTriggerEnter2DEvent>();

        Bind<TapEvent>();
        Bind<TwistEvent>();
        Bind<DragStartedEvent>();
        Bind<DragMovedEvent>();
        Bind<DragStoppedEvent>();
        Bind<SwipeStartEvent>();
        Bind<SwipedLeftEvent>();
        Bind<SwipedRightEvent>();
        Bind<SwipeMovedEvent>();
        Bind<SwipeEndEvent>();
        Bind<TouchDownEvent>();
        Bind<TouchStartEvent>();
        Bind<OutsideUITouchStartEvent>();
        Bind<TouchUpEvent>();
        Bind<UITouchUpEvent>();
        Bind<TouchDown1FingerEvent>();
        Bind<TouchStart1FingerEvent>();
        Bind<TouchUp1FingerEvent>();
        Bind<Touch1FingerCancelEvent>();
        Bind<TouchStart2FingersEvent>();
        Bind<TouchDown2FingersEvent>();
        Bind<TouchUp2FingersEvent>();
        Bind<SwipeStart2FingersEvent>();
        Bind<SwipeMoved2FingersEvent>();
        Bind<SwipeEnd2FingersEvent>();
        Bind<PinchStartedEvent>();
        Bind<PinchMovedEvent>();
        Bind<PinchStoppedEvent>();
        Bind<EmptyTapEvent>();
        Bind<IdleStartEvent>();

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
        Bind<Ref<ISelectableLevelField>>();
        Bind<Ref<ICamera>>();
        Bind<Ref<IFollowCamera>>();
        Bind<Ref<ICameraVelocity>>();
        Bind<Ref<IScreenShake>>();
        Bind<Ref<IWeapon>>();
        Bind<Ref<IHook>>();
        Bind<Ref<IHookProjectile>>();
        Bind<Ref<ITouchInput>>();
        Bind<Ref<IPCInput>>();
        Bind<Ref<IMobileInput>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<ISlowTime>>();
        Bind<Ref<IPlayerGrappling>>();
        Bind<Ref<ITileInput>>();
        Bind<Ref<ISaveCreatedLevelButton>>();
        Bind<Ref<ILevelNameInputField>>();
        Bind<Ref<ILevelSelectGridLayoutGroup>>();
        Bind<Ref<IStatusViewContainer>>();
        Bind<Ref<ILevelObjectButtonGridLayoutGroup>>();

        Bind<Refs<ICheckpoint>>();
        Bind<Refs<ILevelObject>>();
        Bind<Refs<ILevelObjectZone>>();

        BindLabeled<Ref<ICharacterDirectionPointer>>(Label.Player);
        BindLabeled<Ref<ICharacterCollisionDirection>>(Label.Player);
        BindLabeled<Ref<ICharacterVelocity>>(Label.Player);
        BindLabeled<Ref<ICharacterRaycastDirection>>(Label.Player);
        BindLabeled<Ref<ICharacterTurnDirection>>(Label.Player);
        BindLabeled<Ref<ICharacterJump>>(Label.Player);
        BindLabeled<Ref<ICharacterAimLine>>(Label.Player);
        BindLabeled<Ref<ICharacterDie>>(Label.Player);
        BindLabeled<Ref<ICharacterSpeed>>(Label.Player);
        BindLabeled<Ref<ICollisionHitDetection>>(Label.Player);
        BindLabeled<Ref<ITriggerHitDetection>>(Label.Player);
        BindLabeled<Ref<IMoveTowards>>(Label.Player);
        BindLabeled<Ref<ICharacterTrail>>(Label.Player);
        BindLabeled<Ref<ICharacterRotateAroundCorner>>(Label.Player);

        BindLabeled<Ref<ITriggerHitDetection>>(Label.HookProjectile);
        BindLabeled<Ref<IMoveTowards>>(Label.HookProjectile);

        BindLabeled<Ref<ILevelObject>>(Label.SelectedLevelObject);
        BindLabeled<Ref<ILevelObject>>(Label.PreviousSelectedLevelObject);

        Bind<IGameStateService, LocalGameStateService>();

        Bind<Ref<GameStateModel>>();

        Bind<CameraStatus>();
        Bind<LevelStatus>();
        Bind<SceneStatus>();
        Bind<ViewContainerStatus>();
        Bind<InputStatus>();
        Bind<WeaponStatus>();
        Bind<PlayerJumpStatus>();
        Bind<PlayerTurnStatus>();
        Bind<PlayerSessionStatsStatus>();
        Bind<LevelNameStatus>();
        Bind<SelectedGridPositionStatus>();
        Bind<RectangulatedTileGridStatus>();
        Bind<ReleasedSinceLevelObjectSpawnStatus>();
        Bind<LevelObjectTranslateStartOffsetStatus>();
        Bind<LevelContainerTransformStatus>();
        Bind<SelectedLevelObjectNodeStatus>();
        Bind<TranslateStartPositionStatus>();

        On<EnterContextSignal>()
            .InstantiateView<ApplicationView>()
            .InstantiateView<DebugInputView>()
            .Do<LoadGameStateCommand>()
            .Do<InstantiateViewPrefabCommand>("Components/TouchInput")
            .Do<InstantiateViewPrefabCommand>("Components/StatusViewContainer")
            .Do<InstantiateViewPrefabCommand>("UI/Canvas/CanvasUI")
            .Do<InstantiateViewInCanvasLayerCommand>("UI/FPSCounterUI", CanvasLayer.UI)
            .AddContext<CameraContext>()
            .Do<SetNextSceneToSceneCommand>(Scenes.MainMenu)
            .GotoState<LoadingContext>();

        OnChild<LoadingContext, GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.MainMenu)
            .GotoState<MainMenuUIContext>();

        OnChild<LoadingContext, GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.LevelSelect)
            .GotoState<LevelSelectContext>();

        OnChild<LoadingContext, GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.LevelEditor)
            .GotoState<LevelEditorContext>();

        OnChild<LoadingContext, GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotSceneCommand>(Scenes.TestLvl)
            .GotoState<GameLevelContext>();

        OnChild<LoadingContext, GoToSceneCompletedEvent>()
            .Do<AbortIfSceneIsNotALevelCommand>()
            .GotoState<GameLevelContext>();

        On<GoToCurrentSceneEvent>()
            .Do<SetNextSceneToCurrentSceneCommand>()
            .GotoState<LoadingContext>();

        On<GoToNextSceneEvent>()
            .Do<SetNextSceneToNextSceneCommand>()
            .GotoState<LoadingContext>();

        On<GoToSceneEvent>()
            .Do<SetNextSceneCommand>()
            .GotoState<LoadingContext>();

        On<ApplicationPauseEvent>()
            .Do<SaveGameStateCommand>();

        if (Application.isEditor) {
            On<ApplicationQuitEvent>()
                .Do<SaveGameStateCommand>();
        }

    }

}