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

    public void IncrementLevelObjectGridPosition(Vector2 increment) {
        if (OnLevelObjectIncrementGridPosition != null) {
            OnLevelObjectIncrementGridPosition(increment);
        }
    }

    public void IncrementLevelObjectSectionGridPosition(Vector2 increment) {
        gridPosition += increment;
        LevelEditorLevelObjectSectionGrid.Instance.SetLevelObjectSection(gridPosition, this);
    }

    public bool LevelObjectCanMoveToDirection(Vector2 direction, int moveAmount) {
        if(OnLevelObjectCanMoveToDirection == null) { return false; }
        bool canMove = OnLevelObjectCanMoveToDirection(direction, moveAmount);
        return canMove;
    }

    public void DestroyLevelObject() {
        if(OnLevelObectDestroy != null) {
            OnLevelObectDestroy();
        }
    }

}
