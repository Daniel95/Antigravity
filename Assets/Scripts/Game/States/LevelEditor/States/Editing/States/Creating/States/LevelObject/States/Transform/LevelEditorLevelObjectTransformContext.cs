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
            .Do<LevelEditorRemoveRigidBodyFromPreviousSelectedLevelObjectCommand>()
            .Do<LevelEditorRemoveCollisionHitDetectionViewFromPreviousSelectedLevelObjectCommand>();

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<WaitFramesCommand>(1)
            .Do<LevelEditorAddRigidBodyToSelectedLevelObjectCommand>()
            .Do<LevelEditorAddCollisionHitDetectionViewToSelectedLevelObjectCommand>();

        On<LevelEditorLevelObjectCollisionEnter2DEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCannotCollideWithLevelObjectsCommand>()
            .Do<AbortIfGameObjectIsNotALevelObjectCommand>()
            .Do<DebugLogMessageCommand>("Ignore levelObject")
            .Do<LevelEditorSelectedLevelObjectIgnoreColliderOfCollisionCommand>();

        On<LevelEditorLevelObjectCollisionEnter2DEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCannotCollideWithTilesCommand>()
            .Do<AbortIfGameObjectIsNotATileCommand>()
            .Do<DebugLogMessageCommand>("Ignore tile")
            .Do<LevelEditorSelectedLevelObjectIgnoreColliderOfCollisionCommand>();

    }

}