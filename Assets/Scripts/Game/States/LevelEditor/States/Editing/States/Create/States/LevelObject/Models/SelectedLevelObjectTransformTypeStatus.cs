using IoCPlus;
using UnityEngine;

public class SelectedLevelObjectTransformTypeStatusView : View {

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
