using IoCPlus;

public class LevelEditorSelectedLevelObjectTransformTypeStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectTransformTypeStatusUpdatedEvent selectedLevelObjectTransformTypeStatusUpdatedEvent;

    public static LevelObjectTransformType LevelObjectTransformType {
        get {
            return levelObjectTransformType;
        }
        set {
            levelObjectTransformType = value;
            selectedLevelObjectTransformTypeStatusUpdatedEvent.Dispatch();
        }
    }

    private static LevelObjectTransformType levelObjectTransformType;

}
