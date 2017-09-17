using IoCPlus;

public class LevelEditorSavingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/LevelNameInputField", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/GoToNavigatingButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/SaveButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/LevelNameInputField", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/GoToNavigatingButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/SaveButtonUI", CanvasLayer.UI);

        On<LevelEditorSavingLevelNameInputFieldValueChangedEvent>()
            .Do<AbortIfCharacterCountIsLowerThenIntCommand>(3)
            .Do<LevelEditorSavingEnableSaveButtonCommand>(true);

        On<LevelEditorSavingLevelNameInputFieldValueChangedEvent>()
            .Do<AbortIfCharacterCountIsHigherThenIntCommand>(2)
            .Do<LevelEditorSavingEnableSaveButtonCommand>(false);

        On<LevelEditorSavingSaveButtonClickedEvent>()
            .Do<SaveGridPositionsCommand>();

    }

}