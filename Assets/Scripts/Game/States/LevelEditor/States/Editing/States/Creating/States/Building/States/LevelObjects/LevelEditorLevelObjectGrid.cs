using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelObjectGrid : LevelEditorGridPositions {

    public static LevelEditorLevelObjectGrid Instance { get { return GetInstance(); } }

    public Dictionary<Vector2, LevelObject> Grid { get { return levelObjectgrid; } }

    private static LevelEditorLevelObjectGrid instance;

    private Dictionary<Vector2, LevelObject> levelObjectgrid = new Dictionary<Vector2, LevelObject>();

    public LevelObject GetLevelObject(Vector2 gridPosition) {
        return levelObjectgrid[gridPosition];
    }

    public void SetLevelObject(Vector2 gridPosition, LevelObject levelObject) {
        if (levelObjectgrid.ContainsKey(gridPosition)) {
            RemoveLevelObject(gridPosition);
            AddLevelObject(gridPosition, levelObject);
        } else {
            levelObjectgrid[gridPosition] = levelObject;
        }
    }

    public void ReplaceLevelObject(Vector2 gridPosition, LevelObject levelObject) {
        LevelObject oldTLevelObject = levelObjectgrid[gridPosition];
        oldTLevelObject.Destroy();
        levelObjectgrid[gridPosition] = levelObject;
    }

    public void AddLevelObject(Vector2 gridPosition, LevelObject levelObject) {
        Add(gridPosition);
        levelObjectgrid.Add(gridPosition, levelObject);
    }

    public override void Clear() {
        base.Clear();
        foreach (Vector2 gridPosition in levelObjectgrid.Keys) {
            levelObjectgrid[gridPosition].Destroy();
        }
        levelObjectgrid.Clear();
    }

    public void RemoveLevelObject(Vector2 gridPosition) {
        Remove(gridPosition);
        levelObjectgrid[gridPosition].Destroy();
        levelObjectgrid.Remove(gridPosition);
    }

    private static LevelEditorLevelObjectGrid GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<LevelEditorLevelObjectGrid>();
        }
        return instance;
    }

}
