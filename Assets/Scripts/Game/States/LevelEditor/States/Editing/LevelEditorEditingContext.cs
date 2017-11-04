using IoCPlus;

public class LevelEditorEditingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .GotoState<LevelEditorCreatingContext>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToSavingStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorLevelNameStatusLoadedLevelNameIsNullOrEmptyCommand>()
            .Do<LevelEditorLoadLevelSaveDataCommand>();

        On<LeaveContextSignal>()
            .Do<LevelEditorClearGridCommand>()
            .Do<LevelEditorClearLevelObjectsCommand>()
            .Do<LevelEditorClearLevelNameStatusCommand>()
            .Do<ResetCamerPositionCommand>()
            .Do<ResetCameraOrthographicSizeToSystemDefaultCommand>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToSavingStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Creating)
            .GotoState<LevelEditorCreatingContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Navigating)
            .GotoState<LevelEditorNavigatingContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Saving)
            .GotoState<LevelEditorSavingContext>();

        OnChild<LevelEditorSavingContext, LevelEditorSaveLevelEvent>()
            .GotoState<LevelEditorCreatingContext>();

    }

}