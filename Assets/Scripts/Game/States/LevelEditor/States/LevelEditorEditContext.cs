using IoCPlus;

public class LevelEditorEditContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        LevelEditorEditSaveButtonClickedEvent levelEditorEditSaveButtonClickedEvent = Bind<LevelEditorEditSaveButtonClickedEvent>();

        On<EnterContextSignal>()
            .Do<SetCameraOrthographicSizeCommand>(5)
            .GotoState<LevelEditorCreatingContext>()
            .Do<InstantiateListenerViewInCanvasLayerCommand>("UI/LevelEditor/Edit/SaveButtonUI", CanvasLayer.UI, levelEditorEditSaveButtonClickedEvent);

        On<LeaveContextSignal>()
            .Do<SetCameraOrthographicSizeCommand>(10)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Edit/SaveButtonUI", CanvasLayer.UI);

        OnChild<LevelEditorNavigatingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Creating)
            .GotoState<LevelEditorCreatingContext>();

        OnChild<LevelEditorCreatingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Navigating)
            .GotoState<LevelEditorNavigatingContext>();

        On<LevelEditorEditSaveButtonClickedEvent>()
            .Do<SerializeTileGridCommand>()
            .Do<CombineTilesToPrefabAndSaveCommand>();

    }

}