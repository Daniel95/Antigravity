using IoCPlus;
using UnityEngine;

public class LevelEditorSelectedLevelObjectTransformTypeStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent selectedLevelObjectTransformTypeStatusUpdatedEvent;

    public static LevelObjectTransformType? TransformType {
        get {
            return levelObjectTransformType;
        }
        set {
            levelObjectTransformType = value;
            selectedLevelObjectTransformTypeStatusUpdatedEvent.Dispatch();
        }
    }

    private static LevelObjectTransformType? levelObjectTransformType;

}
