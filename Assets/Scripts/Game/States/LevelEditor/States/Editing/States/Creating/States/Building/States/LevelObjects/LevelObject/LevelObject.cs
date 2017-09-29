using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelObject {

    public Transform Transform { get { return GameObject.transform; } }

    public GameObject GameObject;
    public Vector2 LevelObjectNodePosition;
    public List<LevelObjectSection> LevelObjectSections;
    public Vector2 Size;

    private Vector2 nodePosition { get { return Transform.position; }  set { Transform.position = value; } }

    public void Initiate(List<LevelObjectSection> levelObjectSections) {
        LevelObjectSections = levelObjectSections;
        foreach (LevelObjectSection levelObjectSection in LevelObjectSections) {
            levelObjectSection.OnLevelObjectIncrementGridPosition += IncrementLevelObjectGridPosition;
            levelObjectSection.OnLevelObectDestroy += OnDestroy;
        }
    }

    public void IncrementLevelObjectGridPosition(Vector2 incrementalGridPosition) {
        LevelObjectSection levelObjectSection = LevelObjectSections.Find(x => !CheckGridPositionAvailability(x.GridPosition));
        bool newLevelObjectSectionIsOccupied = levelObjectSection != null;
        if(newLevelObjectSectionIsOccupied) { return; }

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
