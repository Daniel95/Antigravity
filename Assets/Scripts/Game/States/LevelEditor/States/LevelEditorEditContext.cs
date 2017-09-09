using IoCPlus;

public class LevelEditorEditContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .GotoState<LevelEditorCreatingContext>()
            .Do<InstantiateListenerViewInCanvasLayerCommand>("UI/LevelEditor/Edit/SaveButtonUI", CanvasLayer.UI, Bind<LevelEditorEditSaveButtonClickedEvent>());

        On<LeaveContextSignal>()
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