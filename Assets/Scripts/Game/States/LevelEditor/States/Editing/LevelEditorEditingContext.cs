using IoCPlus;

public class LevelEditorEditingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetCameraOrthographicSizeCommand>(5)
            .GotoState<LevelEditorCreatingContext>()
            .Do<LoadGridPositionsCommand>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToSavingStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<SetCameraOrthographicSizeCommand>(10)
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

    }

}