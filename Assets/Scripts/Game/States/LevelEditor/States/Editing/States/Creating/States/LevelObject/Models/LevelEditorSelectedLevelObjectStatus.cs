using IoCPlus;
using UnityEngine;

public class LevelEditorSelectedLevelObjectStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectStatusUpdatedEvent selectedLevelObjectStatusUpdatedEvent;

    public static GameObject LevelObject {
        get { return levelObject; }
        set {
            levelObject = value;
            selectedLevelObjectStatusUpdatedEvent.Dispatch();
        }
    }

    private static GameObject levelObject;

}
