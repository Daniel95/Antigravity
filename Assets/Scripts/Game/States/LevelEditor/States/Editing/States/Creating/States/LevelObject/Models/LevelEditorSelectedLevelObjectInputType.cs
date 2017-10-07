using IoCPlus;

public class LevelEditorSelectedLevelObjectTransformTypeStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectInputTypeStatusUpdatedEvent selectedLevelObjectInputTypeStatusUpdatedEvent;

    public static LevelObjectTransformType LevelObjectTransformType {
        get {
            return levelObjectInputType;
        }
        set {
            levelObjectInputType = value;
            selectedLevelObjectInputTypeStatusUpdatedEvent.Dispatch();
        }
    }

    private static LevelObjectTransformType levelObjectInputType;

}
