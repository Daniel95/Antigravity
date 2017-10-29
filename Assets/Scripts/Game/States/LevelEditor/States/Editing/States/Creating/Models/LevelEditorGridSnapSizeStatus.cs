using IoCPlus;
using UnityEngine;

public class LevelEditorGridSnapSizeStatus : StatusView {

    [Inject] private static LevelEditorGridSnapSizeStatusUpdatedEvent gridSnapSizeStatusUpdatedEvent;

    public static Vector2 Size {
        get { return size; }
        set {
            Debug.Log(value);
            previousSize = size;
            size = value;
            gridSnapSizeStatusUpdatedEvent.Dispatch();
        }
    }
    public static Vector2 PreviousSize { get { return previousSize; } }

    private static Vector2 size = Vector2.zero;
    private static Vector2 previousSize = Vector2.zero;

}
