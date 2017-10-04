using IoCPlus;

public class LevelEditorSelectedLevelObjectSectionStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectSectionStatusUpdatedEvent selectedLevelObjectSectionStatusUpdatedEvent;

    public static LevelObjectSection LevelObjectSection {
        get {
            return levelObjectSection;
        }
        set {
            levelObjectSection = value;
            selectedLevelObjectSectionStatusUpdatedEvent.Dispatch();
        }
    }

    private static LevelObjectSection levelObjectSection;

}
