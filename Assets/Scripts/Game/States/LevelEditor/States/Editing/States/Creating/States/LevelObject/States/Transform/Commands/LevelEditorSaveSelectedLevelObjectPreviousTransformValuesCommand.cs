using IoCPlus;
using UnityEngine;

public class LevelEditorSaveSelectedLevelObjectPreviousTransformValuesCommand : Command {

    protected override void Execute() {
        Transform selectedLevelObjectTransform = LevelEditorSelectedLevelObjectStatus.LevelObject.transform;
        DebugHelper.LogPreciseVector(selectedLevelObjectTransform.position, "previous position " + FrameHelper.FrameCount);
        LevelEditorSelectedLevelObjectStatus.PreviousPosition = selectedLevelObjectTransform.position;
        LevelEditorSelectedLevelObjectStatus.PreviousScale = selectedLevelObjectTransform.localScale;
        LevelEditorSelectedLevelObjectStatus.PreviousRotation = selectedLevelObjectTransform.rotation;
    }

}
