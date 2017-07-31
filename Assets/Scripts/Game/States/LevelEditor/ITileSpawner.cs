using UnityEngine;

public interface ITileSpawner {

    void StartSelectionField(Vector2 selectionFieldStartWorldPosition);
    void UpdateSelectionField(Vector2 selectionFieldEndWorldPosition);
    void FinishSelectionField();
    void SpawnTileAtScreenPosition(Vector2 screenPosition);
    void SpawnTileAtWorldPosition(Vector2 worldPosition);

}
