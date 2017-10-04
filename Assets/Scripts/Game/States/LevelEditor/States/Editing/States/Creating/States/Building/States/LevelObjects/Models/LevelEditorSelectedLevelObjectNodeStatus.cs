using IoCPlus;

public class LevelEditorSelectedLevelObjectNodeViewStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectNodeStatusUpdatedEvent selectedLevelObjectNodeStatusUpdatedEvent;

    public static GenerateableLevelObjectNode LevelObjectNode {
        get {
            return levelObjectNode;
        }
        set {
            levelObjectNode = value;
            selectedLevelObjectNodeStatusUpdatedEvent.Dispatch();
        }
    }

    private static GenerateableLevelObjectNode levelObjectNode;

}
