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
            levelObjectSection.Initiate(levelObjectSectionGridPosition);
            levelObjectSections.Add(levelObjectSection);
            levelObjectSection.OnLevelObjectIncrementGridPosition += IncrementLevelObjectGridPosition;
            levelObjectSection.OnLevelObectDestroy += OnDestroy;
        }
    }

    public void IncrementLevelObjectGridPosition(Vector2 incrementalGridPosition) {
        if(!CheckAllIncrementedGridPositionAvailability(incrementalGridPosition)) { return; }

        gameObject.transform.position += (Vector3)LevelEditorGridHelper.GridToNodeVector(incrementalGridPosition);

        List<Vector2> previousLevelObjectSectionGridPositions = levelObjectSections.Select(x => x.GridPosition).ToList();
        LevelEditorLevelObjectSectionGrid.Instance.RemoveLevelObjectSections(previousLevelObjectSectionGridPositions);

        levelObjectSections.ForEach(x => x.IncrementLevelObjectSectionGridPosition(incrementalGridPosition));
    }

    private bool CheckAllIncrementedGridPositionAvailability(Vector2 incrementalGridPosition) {
        LevelObjectSection unavailabeLevelObjectSection = levelObjectSections.Find(x => !CheckGridPositionAvailability(x.GridPosition + incrementalGridPosition));
        bool newLevelObjectSectionIsOccupied = unavailabeLevelObjectSection != null;
        return !newLevelObjectSectionIsOccupied;
    }

    private bool CheckGridPositionAvailability(Vector2 gridPosition) {
        if (!LevelEditorLevelObjectSectionGrid.Instance.Contains(gridPosition)) { return true; }

        List<Vector2> levelObjectSectionPositions = levelObjectSections.Select(x => x.GridPosition).ToList();
        bool occupiedByThisLevelObject = levelObjectSectionPositions.Contains(gridPosition);

        return occupiedByThisLevelObject;
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
