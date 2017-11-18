using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorSelectedLevelObjectStatus : StatusView {

    [Inject] private static LevelEditorSelectedLevelObjectStatusUpdatedEvent selectedLevelObjectStatusUpdatedEvent;

    public static GameObject LevelObject {
        get { return levelObject; }
        set {
            previousLevelObject = levelObject;
            levelObject = value;
            selectedLevelObjectStatusUpdatedEvent.Dispatch();
        }
    }

    public static GameObject PreviousLevelObject { get { return previousLevelObject; } }

    public static Collider2D LevelObjectCollider;
    public static Rigidbody2D LevelObjectRigidBody;

    private static GameObject levelObject;
    private static GameObject previousLevelObject;

}
