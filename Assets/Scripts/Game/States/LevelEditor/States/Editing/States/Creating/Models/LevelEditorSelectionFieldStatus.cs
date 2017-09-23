using System.Collections.Generic;
using UnityEngine;

public class LevelEditorSelectionFieldStatus {

    public bool Enabled;

    public List<Vector2> SelectionFieldGridPositions = new List<Vector2>();
    public List<Vector2> PreviousSelectionFieldGridPositions = new List<Vector2>();
    public Vector2 SelectionFieldStartGridPosition;
    public Vector2 SelectionFieldEndGridPosition;

}
