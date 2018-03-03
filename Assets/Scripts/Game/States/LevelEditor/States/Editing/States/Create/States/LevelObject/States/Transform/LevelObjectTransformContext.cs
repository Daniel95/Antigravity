using IoCPlus;

public class LevelObjectTransformContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<LevelObjectTransformTypeButtonClickedEvent>()
            .Do<UpdateSelectedLevelObjectTransformTypeStatusCommand>();

        On<SelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfSelectedLevelObjectTransformTypeIsNullCommand>()
            .Do<AbortIfSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Translate)
            .GotoState<LevelObjectTranslateContext>();

        On<SelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfSelectedLevelObjectTransformTypeIsNullCommand>()
            .Do<AbortIfSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Scale)
            .GotoState<LevelObjectScaleContext>();

        On<SelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfSelectedLevelObjectTransformTypeIsNullCommand>()
            .Do<AbortIfSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Rotate)
            .GotoState<LevelObjectRotateContext>();

        On<LoadLevelSaveDataCommand>()
            .Do<AbortIfSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfSelectedLevelObjectCanCollideWithLevelObjectsCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorLevelObject, true);

        On<LoadLevelSaveDataCommand>()
            .Do<AbortIfSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfSelectedLevelObjectCannotCollideWithLevelObjectsCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorLevelObject, false);

        On<LoadLevelSaveDataCommand>()
            .Do<AbortIfSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfSelectedLevelObjectCanCollideWithTilesCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorTile, true);

        On<LoadLevelSaveDataCommand>()
            .Do<AbortIfSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfSelectedLevelObjectCannotCollideWithTilesCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorTile, false);

    }

}