using IoCPlus;
using UnityEngine;

public class GridSnapSizeStatus : StatusView {

    [Inject] private static GridSnapSizeStatusUpdatedEvent gridSnapSizeStatusUpdatedEvent;

    public static Vector2 Size {
        get { return size; }
        set {
            previousSize = size;
            size = value;
            gridSnapSizeStatusUpdatedEvent.Dispatch();
        }
    }
    public static Vector2 PreviousSize { get { return previousSize; } }

    private static Vector2 size = Vector2.zero;
    private static Vector2 previousSize = Vector2.zero;

}
