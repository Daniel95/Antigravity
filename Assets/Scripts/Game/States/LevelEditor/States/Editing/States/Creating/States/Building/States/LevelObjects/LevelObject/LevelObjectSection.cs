using System;
using UnityEngine;

[Serializable]
public class LevelObjectSection {

    public Vector2 GridPosition { get { return gridPosition; } }

    public Action OnLevelObectDestroy;
    public Action<Vector2> OnLevelObjectIncrementGridPosition;
    public Func<Vector2, int, bool> OnLevelObjectCanMoveToDirection;

    private Vector2 gridPosition;

    public void Initiate(Vector2 gridPosition) {
        this.gridPosition = gridPosition;
        LevelEditorLevelObjectSectionGrid.Instance.AddLevelObjectSection(gridPosition, this);
    }

    public void SetGridPosition(Vector2 newGridPosition) {
        Vector2 offset = newGridPosition - gridPosition;

        if (OnLevelObjectIncrementGridPosition != null) {
            OnLevelObjectIncrementGridPosition(offset);
        }
    }

    public void IncrementLevelObjectSectionGridPosition(Vector2 increment) {
        gridPosition += increment;
        LevelEditorLevelObjectSectionGrid.Instance.SetLevelObjectSection(gridPosition, this);
    }

    public void DestroyLevelObject() {
        if(OnLevelObectDestroy != null) {
            OnLevelObectDestroy();
        }
    }

}
