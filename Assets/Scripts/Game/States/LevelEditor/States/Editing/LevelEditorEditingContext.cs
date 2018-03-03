using IoCPlus;

public class LevelEditorEditingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .GotoState<LevelEditorCreateContext>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToSavingStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfLevelNameStatusLoadedLevelNameIsNullOrEmptyCommand>()
            .Do<LevelEditorLoadLevelSaveDataCommand>();

        On<LeaveContextSignal>()
            .Do<ClearLevelEditorGridCommand>()
            .Do<LevelEditorClearLevelObjectsCommand>()
            .Do<ClearLevelNameStatusCommand>()
            .Do<ResetCamerPositionCommand>()
            .Do<ResetCameraOrthographicSizeToSystemDefaultCommand>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToSavingStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Create)
            .GotoState<LevelEditorCreateContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Navigate)
            .GotoState<LevelEditorNavigateContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Save)
            .GotoState<SaveCreatedLevelContext>();

        OnChild<SaveCreatedLevelContext, SaveCreatedLevelEvent>()
            .GotoState<LevelEditorCreateContext>();

    }

}