using IoCPlus;
using UnityEngine;

public class SelectedLevelObjectTransformTypeStatus : StatusView {

    [Inject] private static SelectedLevelObjectTransformTypeStatusUpdatedEvent selectedLevelObjectTransformTypeStatusUpdatedEvent;

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
