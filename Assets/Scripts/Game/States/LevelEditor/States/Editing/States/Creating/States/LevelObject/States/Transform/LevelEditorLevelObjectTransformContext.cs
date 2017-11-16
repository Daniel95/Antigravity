using IoCPlus;

public class LevelEditorLevelObjectTransformContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorTranslateStartPositionStatus>>();

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorTranslateStartPositionStatus>>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Translate)
            .GotoState<LevelEditorLevelObjectTranslateContext>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Scale)
            .GotoState<LevelEditorLevelObjectScaleContext>();

        On<LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNotCommand>(LevelObjectTransformType.Rotate)
            .GotoState<LevelEditorLevelObjectRotateContext>();

        /*
        On<LevelEditorLevelObjectCollisionEnter2DEvent>()
            .Do<AbortIfLevelEditorCollisionIsALevelObjectCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanNotCollideWithTilesCommand>()
            .Do<LevelEditorResetSelectedLevelObjectTransformToPreviousTransformValuesCommand>()
            .Dispatch<LevelEditorSelectedLevelObjectTransformWasResetEvent>();

        On<LevelEditorLevelObjectCollisionEnter2DEvent>()
            .Do<AbortIfLevelEditorCollisionIsNotALevelObjectCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanNotCollideWithLevelObjectsCommand>()
            .Do<LevelEditorResetSelectedLevelObjectTransformToPreviousTransformValuesCommand>()
            .Dispatch<LevelEditorSelectedLevelObjectTransformWasResetEvent>();

        On<LevelEditorLevelObjectTriggerEnter2DEvent>()
            .Do<AbortIfLevelEditorColliderIsNotALevelObjectCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanNotCollideWithLevelObjectsCommand>()
            .Do<LevelEditorResetSelectedLevelObjectTransformToPreviousTransformValuesCommand>()
            .Dispatch<LevelEditorSelectedLevelObjectTransformWasResetEvent>();

      //  On<LevelEditorSelectedLevelObjectTransformWasResetEvent>()
        //    .Do<LevelEditorResetSelectedLevelObjectStatusCommand>();
    */
    }

}