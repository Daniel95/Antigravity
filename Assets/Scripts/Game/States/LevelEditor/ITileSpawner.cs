using UnityEngine;

public interface ITileSpawner {

    void SpawnTileAtScreenPosition(Vector2 screenPosition);
    void SpawnTileAtWorldPosition(Vector2 worldPosition);

}
