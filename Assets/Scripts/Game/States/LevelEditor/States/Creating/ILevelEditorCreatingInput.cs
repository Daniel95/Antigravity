using System.Collections.Generic;
using UnityEngine;

public interface ILevelEditorCreatingInput {

    List<Vector2> SelectionFieldGridPositions { get; }
    List<Vector2> PreviousSelectionFieldGridPositions { get; }

    void StartSelectionField(Vector2 selectionFieldStartWorldPosition);
    void UpdateSelectionField(Vector2 selectionFieldEndWorldPosition);
    void ClearSelectionField();
    void RemoveTilesInSelectionField();
    void ReplaceNewTilesInSelectionField();
    void SpawnTileAtScreenPosition(Vector2 screenPosition);
    void SpawnTileAtWorldPosition(Vector2 worldPosition);
    void SpawnTileAtGridPosition(Vector2 gridPosition);

}
