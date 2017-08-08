using UnityEngine;

public interface ILevelEditorCreatingInput {

    void StartSelectionField(Vector2 selectionFieldStartWorldPosition);
    void UpdateSelectionField(Vector2 selectionFieldEndWorldPosition);
    void ClearSelectionField();
    void RemoveTilesInSelectionField();
    void ReplaceNewTilesInSelectionField();
    void SpawnTileAtScreenPosition(Vector2 screenPosition);
    void SpawnTileAtWorldPosition(Vector2 worldPosition);
    void SpawnTileAtGridPosition(Vector2 gridPosition);

}
