using System;
using UnityEngine;

[Serializable]
public class LevelObjectSection {

    public Vector2 GridPosition { get { return gridPosition; } }
    public OnGridLevelObject LevelObject { get { return GetLevelObject(); } }

    public Action OnLevelObectDestroy;
    public Action<Vector2> OnLevelObjectIncrementGridPosition;

    private Vector2 gridPosition;
    private OnGridLevelObject levelObject;

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

    private OnGridLevelObject GetLevelObject() {
        if(levelObject == null) {
            levelObject = LevelEditorLevelObjectSectionGrid.Instance.GetLevelObject(gridPosition);
        }
        return LevelEditorLevelObjectSectionGrid.Instance.GetLevelObject(gridPosition);
    }

}
