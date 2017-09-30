﻿using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGridPositions : MonoBehaviour {

    public static HashSet<Vector2> GridPositions { get { return gridPositions; } }

    private static HashSet<Vector2> gridPositions = new HashSet<Vector2>();

    public bool Contains(Vector2 gridPosition) {
        return gridPositions.Contains(gridPosition);
    }

    public virtual void Clear() {
        gridPositions.Clear();
    }

    protected virtual void Add(Vector2 gridPosition) {
        gridPositions.Add(gridPosition);
    }

    protected virtual void Remove(Vector2 gridPosition) {
        gridPositions.Remove(gridPosition);
    }

}