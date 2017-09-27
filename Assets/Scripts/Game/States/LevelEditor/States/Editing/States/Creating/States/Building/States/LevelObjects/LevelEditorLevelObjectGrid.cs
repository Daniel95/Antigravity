using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelObjectGrid : LevelEditorGridPositions {

    public static LevelEditorLevelObjectGrid Instance { get { return GetInstance(); } }

    public Dictionary<Vector2, LevelObject> Grid { get { return grid; } }

    private static LevelEditorLevelObjectGrid instance;

    private Dictionary<Vector2, LevelObject> grid = new Dictionary<Vector2, LevelObject>();

    public LevelObject GetLevelObject(Vector2 gridPosition) {
        return grid[gridPosition];
    }

    public void SetLevelObject(Vector2 gridPosition, LevelObject levelObject) {
        if (grid.ContainsKey(gridPosition)) {
            RemoveLevelObject(gridPosition);
            AddLevelObject(gridPosition, levelObject);
        } else {
            grid[gridPosition] = levelObject;
        }
    }

    public void ReplaceLevelObject(Vector2 gridPosition, LevelObject levelObject) {
        LevelObject oldTLevelObject = grid[gridPosition];
        oldTLevelObject.Destroy();
        grid[gridPosition] = levelObject;
    }

    public void AddLevelObject(Vector2 gridPosition, LevelObject levelObject) {
        Add(gridPosition);
        grid.Add(gridPosition, levelObject);
    }

    public override void Clear() {
        base.Clear();
        foreach (Vector2 gridPosition in grid.Keys) {
            grid[gridPosition].Destroy();
        }
        grid.Clear();
    }

    public void RemoveLevelObject(Vector2 gridPosition) {
        Remove(gridPosition);
        grid[gridPosition].Destroy();
        grid.Remove(gridPosition);
    }

    private static LevelEditorLevelObjectGrid GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<LevelEditorLevelObjectGrid>();
        }
        return instance;
    }

}
