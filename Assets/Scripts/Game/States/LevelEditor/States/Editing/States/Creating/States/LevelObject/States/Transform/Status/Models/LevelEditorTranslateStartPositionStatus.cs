using IoCPlus;
using UnityEngine;

public class LevelEditorTranslateStartPositionStatus : StatusView {

    [Inject] private static LevelEditorTranslateStartWorldPositionStatusUpdatedEvent translateStartWorldPositionUpdatedEvent;

    public static Vector2 StartWorldPosition {
        get { return startWorldPosition; }
        set {
            startWorldPosition = value;
            translateStartWorldPositionUpdatedEvent.Dispatch();
        }
    }

    private static Vector2 startWorldPosition;


}
