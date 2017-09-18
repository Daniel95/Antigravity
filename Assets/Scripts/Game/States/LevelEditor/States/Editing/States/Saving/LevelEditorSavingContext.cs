using IoCPlus;

public class LevelEditorSavingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/LevelNameInputField", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/GoToNavigatingButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/SaveButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/LevelNameInputField", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/GoToNavigatingButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/SaveButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<LevelEditorSavingLevelNameInputFieldValueChangedEvent>()
            .Do<AbortIfCharacterCountIsLowerThenIntCommand>(3)
            .Do<AbortIfLevelIsNewButLevelNameAlreadyExistsCommand>()
            .Do<LevelEditorSavingEnableSaveButtonCommand>(true)
            .OnAbort<LevelEditorSavingEnableSaveButtonFalseCommand>();

        On<LevelEditorSavingSaveButtonClickedEvent>()
            .Do<SaveGridPositionsCommand>();

    }

}