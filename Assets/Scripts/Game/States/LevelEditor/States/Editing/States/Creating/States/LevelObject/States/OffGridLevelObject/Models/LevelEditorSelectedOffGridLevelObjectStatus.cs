using IoCPlus;
using UnityEngine;

public class LevelEditorSelectedOffGridLevelObjectStatus : StatusView {

    [Inject] private static LevelEditorSelectedOffGridLevelObjectStatusUpdatedEvent selectedOffGridLevelObjectStatusUpdatedEvent;

    public static GameObject OffGridLevelObject {
        get { return offGridLevelObject; }
        set {
            selectedOffGridLevelObjectStatusUpdatedEvent.Dispatch();
            offGridLevelObject = value;
        }
    }

    private static GameObject offGridLevelObject;

}
