using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorSelectionFieldStatusView : StatusView {

    public static bool Enabled {
        get {
            return enabled;
        }
        set {
            enabled = value;
            selectionFieldEnabledUpdatedEvent.Dispatch();
        }
    }

    public static List<Vector2> SelectionFieldGridPositions = new List<Vector2>();
    public static List<Vector2> PreviousSelectionFieldGridPositions = new List<Vector2>();
    public static Vector2 SelectionFieldStartGridPosition;
    public static Vector2 SelectionFieldEndGridPosition;

    [Inject] private static LevelEditorSelectionFieldEnabledUpdatedEvent selectionFieldEnabledUpdatedEvent;
    //[Inject] private static LevelEditorSelectionFieldEnabledUpdatedEvent selectionFieldEnabledUpdatedEvent;

    private new static bool enabled;

}
