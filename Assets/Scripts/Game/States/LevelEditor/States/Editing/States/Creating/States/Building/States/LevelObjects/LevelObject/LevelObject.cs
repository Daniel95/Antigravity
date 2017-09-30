using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelObject {

    public Transform Transform { get { return gameObject.transform; } }
    public Vector2 GridPosition { get { return LevelEditorGridHelper.NodeToGridPosition(nodePosition); } }

    private Vector2 nodePosition { get { return Transform.position; }  set { Transform.position = value; } }
    private GameObject gameObject;
    private List<LevelObjectSection> levelObjectSections = new List<LevelObjectSection>();


    public void Initiate(List<Vector2> levelObjectSectionGridPositions, GameObject gameObject) {
        this.gameObject = gameObject;

        foreach (Vector2 levelObjectSectionGridPosition in levelObjectSectionGridPositions) {
            LevelObjectSection levelObjectSection = new LevelObjectSection();
            levelObjectSection.SetLevelObjectSectionGridPosition(levelObjectSectionGridPosition);
            levelObjectSections.Add(levelObjectSection);
            levelObjectSection.OnLevelObjectIncrementGridPosition += IncrementLevelObjectGridPosition;
            levelObjectSection.OnLevelObectDestroy += OnDestroy;
        }
    }

    public void IncrementLevelObjectGridPosition(Vector2 incrementalGridPosition) {
        Dictionary<Vector2, LevelObjectSection> levelObjectSectionsGrid = GetLevelObjectSectionsGrid();
        List<Vector2> incremetedLevelobjectGridPositions = levelObjectSectionsGrid.Select(x => x.Key + incrementalGridPosition).ToList();
        if(!CheckGridPositionsAvailability(incremetedLevelobjectGridPositions)) { return; }

        gameObject.transform.position += (Vector3)LevelEditorGridHelper.GridToNodeVector(incrementalGridPosition);

        List<Vector2> previousLevelObjectSectionGridPositions = levelObjectSectionsGrid.Keys.ToList();
        LevelEditorLevelObjectSectionGrid.Instance.RemoveLevelObjectSections(previousLevelObjectSectionGridPositions);

        foreach (KeyValuePair<Vector2, LevelObjectSection> levelObjectGridNode in levelObjectSectionsGrid) {
            Vector2 newGridPosition = levelObjectGridNode.Key + incrementalGridPosition;
            levelObjectGridNode.Value.SetLevelObjectSectionGridPosition(newGridPosition);
        }
    }

    private bool CheckGridPositionsAvailability(List<Vector2> gridPositions) {
        bool unavailable = gridPositions.Exists(x => !CheckGridPositionAvailability(x));
        return !unavailable;
    }

    private bool CheckGridPositionAvailability(Vector2 gridPosition) {
        if (!LevelEditorLevelObjectSectionGrid.Instance.Contains(gridPosition)) { return true; }

        List<Vector2> levelObjectSectionPositions = levelObjectSections.Select(x => x.GridPosition).ToList();
        bool occupiedByThisLevelObject = levelObjectSectionPositions.Contains(gridPosition);

        return occupiedByThisLevelObject;
    }

    private Dictionary<Vector2, LevelObjectSection> GetLevelObjectSectionsGrid() {
        Dictionary<Vector2, LevelObjectSection> levelObjectSectionsGrid = new Dictionary<Vector2, LevelObjectSection>();
        foreach (LevelObjectSection levelObjectSection in levelObjectSections) {
            levelObjectSectionsGrid.Add(levelObjectSection.GridPosition, levelObjectSection);
        }
        return levelObjectSectionsGrid;
    }

    private void OnDestroy() {
        Object.Destroy(gameObject);

        foreach (LevelObjectSection levelObjectSection in levelObjectSections) {
            LevelEditorLevelObjectSectionGrid.Instance.RemoveLevelObjectSection(levelObjectSection.GridPosition);

            levelObjectSection.OnLevelObjectIncrementGridPosition -= IncrementLevelObjectGridPosition;
            levelObjectSection.OnLevelObectDestroy -= OnDestroy;
        }
    }

}
