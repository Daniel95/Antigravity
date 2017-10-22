using IoCPlus;
using UnityEngine;

public class LevelEditorSelectionFieldSnapSizeStatus : StatusView {

    [Inject] private static LevelEditorSelectionFieldSnapSizeStatusUpdatedEvent selectionFieldSnapSizeStatusUpdatedEvent;

    public static Vector2 Size {
        get { return size; }
        set {
            size = value;
            selectionFieldSnapSizeStatusUpdatedEvent.Dispatch();
        }
    }

    private static Vector2 size = Vector2.one;

}
