using IoCPlus;

public class LevelEditorLevelObjectTransformContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<LevelEditorLevelObjectTransformTypeButtonClickedEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeStatusCommand>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Translate)
            .GotoState<LevelEditorLevelObjectTranslateContext>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Scale)
            .GotoState<LevelEditorLevelObjectScaleContext>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Rotate)
            .GotoState<LevelEditorLevelObjectRotateContext>();

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorPreviousSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsPreviousSelectedLevelObjectCommand>()
            .Do<LevelEditorRemoveRigidBodyFromPreviousSelectedLevelObjectCommand>()
            .Do<LevelEditorRemoveCollisionHitDetectionViewFromPreviousSelectedLevelObjectCommand>();

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<WaitFramesCommand>(1)
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsPreviousSelectedLevelObjectCommand>()
            .Do<LevelEditorAddRigidBodyToSelectedLevelObjectCommand>()
            .Do<LevelEditorAddCollisionHitDetectionViewToSelectedLevelObjectCommand>();

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanCollideWithLevelObjectsCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorLevelObject, true);

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCannotCollideWithLevelObjectsCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorLevelObject, false);

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanCollideWithTilesCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorTile, true);

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCannotCollideWithTilesCommand>()
            .Do<IgnoreLayerCollisionCommand>(Layers.LevelEditorLevelObject, Layers.LevelEditorTile, false);

    }

}