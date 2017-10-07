using System;
using UnityEngine;

[Serializable]
public class LevelObjectSection {

    public Vector2 GridPosition { get { return gridPosition; } }

    public Action OnLevelObectDestroy;
    public Action<Vector2> OnLevelObjectIncrementGridPosition;

    private Vector2 gridPosition;

    public void SetLevelObjectGridPosition(Vector2 newGridPosition) {
        Vector2 offset = newGridPosition - gridPosition;

        if (OnLevelObjectIncrementGridPosition != null) {
            OnLevelObjectIncrementGridPosition(offset);
        }
    }

    public void SetLevelObjectSectionGridPosition(Vector2 gridPosition) {
        this.gridPosition = gridPosition;
        LevelEditorLevelObjectSectionGrid.Instance.AddLevelObjectSection(gridPosition, this);
    }

    public void DestroyLevelObject() {
        if(OnLevelObectDestroy != null) {
            OnLevelObectDestroy();
        }
    }

}
