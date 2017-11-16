using IoCPlus;
using UnityEngine;

public class LevelEditorResetSelectedLevelObjectTransformToPreviousTransformValuesCommand : Command {

    protected override void Execute() {
        Transform selectedLevelObjectTransform = LevelEditorSelectedLevelObjectStatus.LevelObject.transform;
        DebugHelper.LogPreciseVector(LevelEditorSelectedLevelObjectStatus.PreviousPosition, "Reset to " + FrameHelper.FrameCount);
        selectedLevelObjectTransform.position = LevelEditorSelectedLevelObjectStatus.PreviousPosition;
        selectedLevelObjectTransform.localScale = LevelEditorSelectedLevelObjectStatus.PreviousScale;
        selectedLevelObjectTransform.rotation = LevelEditorSelectedLevelObjectStatus.PreviousRotation;
    }

}
