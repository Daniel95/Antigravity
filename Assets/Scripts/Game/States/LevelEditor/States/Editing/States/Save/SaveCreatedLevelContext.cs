using IoCPlus;

public class SaveCreatedLevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/LevelNameInputField", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/GoToNavigatingButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/SaveButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<EnableSaveCreatedLevelButtonCommand>(false);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/LevelNameInputField", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/GoToNavigatingButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Saving/SaveButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/GoToMainMenuStateButtonUI", CanvasLayer.UI);

        On<LevelNameInputFieldValueChangedEvent>()
            .Do<AbortIfCharacterCountIsLowerThenIntCommand>(3)
            .Do<AbortIfLevelIsNewButNameAlreadyExistsCommand>()
            .Do<EnableSaveCreatedLevelButtonCommand>(true)
            .OnAbort<DisableSaveCreatedLevelButtonCommand>();

        On<SaveCreatedLevelButtonClickedEvent>()
            .Do<DispatchSaveLevelEventCommand>();

        On<SaveCreatedLevelEvent>()
            .Do<SaveLevelSaveDataCommand>()
            .Do<UpdateLevelNameStatusCommand>();

    }

}