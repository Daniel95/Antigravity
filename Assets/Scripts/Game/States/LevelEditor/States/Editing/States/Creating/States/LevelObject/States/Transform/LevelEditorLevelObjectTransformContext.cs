using IoCPlus;

public class LevelEditorLevelObjectTransformContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<LevelEditorLevelObjectTransformTypeButtonClickedEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeStatusCommand>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Translate)
            .GotoState<LevelEditorLevelObjectTranslateContext>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Scale)
            .GotoState<LevelEditorLevelObjectScaleContext>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Rotate)
            .GotoState<LevelEditorLevelObjectRotateContext>();

        On<LevelEditorNewLevelObjectSelectedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanCollideWithLevelObjectsCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorLevelObject, true);

        On<LevelEditorNewLevelObjectSelectedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCannotCollideWithLevelObjectsCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorLevelObject, false);

        On<LevelEditorNewLevelObjectSelectedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanCollideWithTilesCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorTile, true);

        On<LevelEditorNewLevelObjectSelectedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCannotCollideWithTilesCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorTile, false);

    }

}