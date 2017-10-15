using IoCPlus;
using UnityEngine;

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

    }

}