using IoCPlus;
using UnityEngine;

public class LevelEditorSelectedLevelObjectStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectStatusUpdatedEvent selectedLevelObjectStatusUpdatedEvent;

    public static GameObject LevelObject {
        get { return levelObject; }
        set {
            previousLevelObject = levelObject;
            levelObject = value;
            levelObjectCollider = levelObject.GetComponent<Collider2D>();
            selectedLevelObjectStatusUpdatedEvent.Dispatch();
        }
    }

    public static Collider2D LevelObjectCollider { get { return levelObjectCollider; } }
    public static GameObject PreviousLevelObject { get { return previousLevelObject; } }

    private static GameObject levelObject;
    private static Collider2D levelObjectCollider;
    private static GameObject previousLevelObject;

}
