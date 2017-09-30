using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorLevelObjectSectionGrid : LevelEditorGridPositions {

    public static LevelEditorLevelObjectSectionGrid Instance { get { return GetInstance(); } }

    public Dictionary<Vector2, LevelObjectSection> LevelObjectSectionsGrid { get { return levelObjectSectiongrid; } }

    private static LevelEditorLevelObjectSectionGrid instance;

    private Dictionary<Vector2, LevelObjectSection> levelObjectSectiongrid = new Dictionary<Vector2, LevelObjectSection>();

    public LevelObjectSection GetLevelObjectSection(Vector2 gridPosition) {
        return levelObjectSectiongrid[gridPosition];
    }

    public void SetLevelObjectSection(Vector2 gridPosition, LevelObjectSection levelObject) {
        if (Contains(gridPosition)) {
            Remove(gridPosition);
            if(levelObjectSectiongrid.ContainsKey(gridPosition)) {
                levelObjectSectiongrid.Remove(gridPosition);
            }
        }
        AddLevelObjectSection(gridPosition, levelObject);
    }

    public void AddLevelObjectSection(Vector2 gridPosition, LevelObjectSection levelObject) {
        Add(gridPosition);
        levelObjectSectiongrid.Add(gridPosition, levelObject);
    }

    public override void Clear() {
        List<Vector2> levelObjectSectionGridPositions = levelObjectSectiongrid.Keys.ToList();
        for (int i = levelObjectSectionGridPositions.Count; i >= 0; i--) {
            Vector2 gridPosition = levelObjectSectionGridPositions[i];
            if(!levelObjectSectiongrid.ContainsKey(gridPosition)) { return; }

            levelObjectSectiongrid[gridPosition].DestroyLevelObject();
        }
    }

    public void RemoveLevelObject(Vector2 gridPosition) {
        levelObjectSectiongrid[gridPosition].DestroyLevelObject();
    }

    public void RemoveLevelObjectSections(List<Vector2> gridPositions) {
        foreach (Vector2 gridPosition in gridPositions) {
            Remove(gridPosition);
            levelObjectSectiongrid.Remove(gridPosition);
        }
    }

    public void RemoveLevelObjectSection(Vector2 gridPosition) {
        Remove(gridPosition);
        levelObjectSectiongrid.Remove(gridPosition);
    }

    private static LevelEditorLevelObjectSectionGrid GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<LevelEditorLevelObjectSectionGrid>();
        }
        return instance;
    }

}
