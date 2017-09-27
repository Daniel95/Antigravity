using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGridPositions : MonoBehaviour {

    public static List<Vector2> GridPositions { get { return gridPositions; } }

    private static List<Vector2> gridPositions;

    public bool Contains(Vector2 gridPosition) {
        bool containsGridPosition = gridPositions.Contains(gridPosition);
        return containsGridPosition;
    }

    public virtual void Add(Vector2 gridPosition) {
        gridPositions.Add(gridPosition);
    }

    public virtual void Remove(Vector2 gridPosition) {
        gridPositions.Remove(gridPosition);
    }

    public virtual void Clear() {
        gridPositions.Clear();
    }

}
