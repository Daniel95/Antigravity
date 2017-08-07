using UnityEngine;

public interface ILevelEditorBuildingInput {

    void StartSelectionField(Vector2 selectionFieldStartWorldPosition);
    void UpdateSelectionField(Vector2 selectionFieldEndWorldPosition);
    void FinishSelectionField();
    void SpawnTileAtScreenPosition(Vector2 screenPosition);
    void SpawnTileAtWorldPosition(Vector2 worldPosition);
    void SpawnTileAtGridPosition(Vector2 gridPosition);

}
