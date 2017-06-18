using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<CharacterTurnToNextDirectionEvent>();
        Bind<CharacterSetMoveDirectionEvent>();
        Bind<PlayerBoostSpeedEvent>();
        Bind<PlayerTemporarySpeedChangeEvent>();
        Bind<CharacterTemporarySpeedDecreaseEvent>();
        Bind<PlayerTemporarySpeedChangeEvent>();
        Bind<CharacterRemoveCollisionDirectionEvent>();
        Bind<CharacterJumpEvent>();
        Bind<CharacterRetryJumpEvent>();
        Bind<PlayerBounceEvent>();
        Bind<CharacterPointToDirectionEvent>();
        Bind<CharacterSetVelocityEvent>();
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
        Bind<Ref<ISelectableLevelField>>();
        Bind<Ref<ICamera>>();
        Bind<Ref<IFollowCamera>>();
        Bind<Ref<IDragCamera>>();
        Bind<Ref<IScreenShake>>();
        Bind<Ref<IWeapon>>();
        Bind<Ref<IHook>>();
        Bind<Ref<IHookProjectile>>();
        Bind<Ref<IPCInput>>();
        Bind<Ref<IMobileInput>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<ISlowTime>>();
        Bind<Ref<IGrapplingState>>();

        // old
        Bind<Ref<ICharacterAimLine>>();
        Bind<Ref<ICharacterJump>>();
        Bind<Ref<ICharacterRaycastDirection>>();
        Bind<Ref<ICharacterTurnDirection>>();
        Bind<Ref<ICharacterVelocity>>();
        Bind<Ref<ICharacterCollisionDirection>>();
        Bind<Ref<ICharacterDirectionPointer>>();
        Bind<Ref<ITriggerHitDetection>>();
        Bind<Ref<ICollisionHitDetection>>();
        //

        /*
        BindLabeled<Ref<ICharacterAimLine>>(Label.Player);
        BindLabeled<Ref<ICharacterJump>>(Label.Player);
        BindLabeled<Ref<ICharacterRaycastDirection>>(Label.Player);
        BindLabeled<Ref<ICharacterTurnDirection>>(Label.Player);
        BindLabeled<Ref<ICharacterVelocity>>(Label.Player);
        BindLabeled<Ref<ICharacterCollisionDirection>>(Label.Player);
        BindLabeled<Ref<ICharacterSurroundingDirection>>(Label.Player);
        BindLabeled<Ref<ICharacterDirectionPointer>>(Label.Player);
        BindLabeled<Ref<ICollisionHitDetection>>(Label.Player);
        */

        //player
        BindLabeled<Ref<ICharacterBounce>>(Label.Player);
        BindLabeled<Ref<ICharacterDie>>(Label.Player);
        BindLabeled<Ref<ICharacterSpeed>>(Label.Player);

        BindLabeled<Ref<ITriggerHitDetection>>(Label.Player);
        BindLabeled<Ref<IMoveTowards>>(Label.Player);

        BindLabeled<Ref<ITriggerHitDetection>>(Label.HookProjectile);
        BindLabeled<Ref<IMoveTowards>>(Label.HookProjectile);


        Bind<IGameStateService, LocalGameStateService>();

        Bind<Ref<GameStateModel>>();

        Bind<LevelStatus>();
        Bind<SceneStatus>();
        Bind<ViewContainerStatus>();
        Bind<InputStatus>();
        Bind<WeaponStatus>();
        Bind<JumpStatus>();

        On<EnterContextSignal>()
            .InstantiateView<ApplicationView>()
            .InstantiateView<DebugInputView>()
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