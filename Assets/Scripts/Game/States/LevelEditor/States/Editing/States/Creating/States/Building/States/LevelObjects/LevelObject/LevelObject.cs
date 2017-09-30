using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelObject {

    public Transform Transform { get { return GameObject.transform; } }

    public GameObject GameObject;
    public Vector2 LevelObjectNodePosition;
    public List<LevelObjectSection> LevelObjectSections;
    public Vector2 GridSize;

    private Vector2 nodePosition { get { return Transform.position; }  set { Transform.position = value; } }
    private Vector2 gridPosition { get { return LevelEditorGridHelper.NodeToGridPosition(nodePosition); } set { nodePosition = LevelEditorGridHelper.GridToNodePosition(value); } }

    public void Initiate(List<LevelObjectSection> levelObjectSections) {
        LevelObjectSections = levelObjectSections;
        foreach (LevelObjectSection levelObjectSection in LevelObjectSections) {
            levelObjectSection.OnLevelObjectIncrementGridPosition += IncrementLevelObjectGridPosition;
            levelObjectSection.OnLevelObectDestroy += OnDestroy;
        }
    }

    public void IncrementLevelObjectGridPosition(Vector2 incrementalGridPosition) {
        LevelObjectSection unavailabeLevelObjectSection = LevelObjectSections.Find(x => !CheckGridPositionAvailability(x.GridPosition));
        bool newLevelObjectSectionIsOccupied = unavailabeLevelObjectSection != null;
        if(newLevelObjectSectionIsOccupied) { return; }

        GameObject.transform.position += (Vector3)LevelEditorGridHelper.GridToNodeVector(incrementalGridPosition);

        List<Vector2> previousLevelObjectSectionGridPositions = LevelObjectSections.Select(x => x.GridPosition).ToList();
        LevelEditorLevelObjectSectionGrid.Instance.RemoveLevelObjectSections(previousLevelObjectSectionGridPositions);

        LevelObjectSections.ForEach(x => x.IncrementLevelObjectSectionGridPosition(incrementalGridPosition));
    }

    private bool CheckGridPositionAvailability(Vector2 gridPosition) {
        if (!LevelEditorLevelObjectSectionGrid.Instance.Contains(gridPosition)) { return true; }

        List<Vector2> levelObjectSectionPositions = LevelObjectSections.Select(x => x.GridPosition).ToList();
        bool occupiedByThisLevelObject = levelObjectSectionPositions.Contains(gridPosition);

        return occupiedByThisLevelObject;
    }

    private void OnDestroy() {
        Object.Destroy(GameObject);

        foreach (LevelObjectSection levelObjectSection in LevelObjectSections) {
            LevelEditorLevelObjectSectionGrid.Instance.RemoveLevelObjectSection(levelObjectSection.GridPosition);

            levelObjectSection.OnLevelObjectIncrementGridPosition -= IncrementLevelObjectGridPosition;
            levelObjectSection.OnLevelObectDestroy -= OnDestroy;
        }
    }

}
