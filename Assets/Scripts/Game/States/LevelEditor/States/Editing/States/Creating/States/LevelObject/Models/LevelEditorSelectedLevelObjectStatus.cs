using IoCPlus;
using System.Collections.Generic;
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

    public static Vector2 PreviousPosition;
    public static Vector2 PreviousScale;
    public static Quaternion PreviousRotation;
    public static List<Collider2D> CollisionColliders = new List<Collider2D>();

    private static GameObject levelObject;

}
