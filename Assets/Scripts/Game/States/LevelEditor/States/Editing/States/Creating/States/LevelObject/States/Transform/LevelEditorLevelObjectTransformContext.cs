using IoCPlus;

public class LevelEditorLevelObjectTransformContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

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

        On<LevelEditorLevelObjectCollisionEnter2DEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanCollideWithLevelObjectsCommand>()
            .Do<AbortIfCollisionGameObjectIsNotALevelObjectCommand>()
            .Do<LevelEditorSelectedLevelObjectIgnoreColliderOfCollisionCommand>();

        On<LevelEditorLevelObjectCollisionEnter2DEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanCollideWithTilesCommand>()
            .Do<AbortIfCollisionGameObjectIsNotATileCommand>()
            .Do<LevelEditorSelectedLevelObjectIgnoreColliderOfCollisionCommand>();

    }

}