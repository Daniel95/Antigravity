using UnityEngine;

public interface ILevelEditorTiles {

    int SpawnLimit { get; }

    void ClearSelectionFieldAvailableGridPositions();
    void RemoveTilesInSelectionField();
    void ReplaceNewTilesInSelectionField();
    void SpawnTileAtScreenPosition(Vector2 screenPosition);
    void SpawnTileAtWorldPosition(Vector2 worldPosition);
    void SpawnTileAtGridPosition(Vector2 gridPosition);

}
