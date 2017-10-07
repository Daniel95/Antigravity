using IoCPlus;

public class LevelEditorSelectedLevelObjectInputTypeStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectInputTypeStatusUpdatedEvent selectedLevelObjectInputTypeStatusUpdatedEvent;

    public static LevelObjectInputType LevelObjectInputType {
        get {
            return levelObjectInputType;
        }
        set {
            levelObjectInputType = value;
            selectedLevelObjectInputTypeStatusUpdatedEvent.Dispatch();
        }
    }

    private static LevelObjectInputType levelObjectInputType;

}
