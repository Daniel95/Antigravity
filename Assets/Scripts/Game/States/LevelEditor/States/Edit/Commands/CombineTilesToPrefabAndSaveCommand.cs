using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CombineTilesToPrefabAndSaveCommand : Command {

    private const string LEVEL_PREFAB_PATH = "LevelEditor/LevelPrefab";

    protected override void Execute() {
        GameObject levelPrefab = Resources.Load<GameObject>(LEVEL_PREFAB_PATH);
        GameObject levelGameObject = Object.Instantiate(levelPrefab);

        Material material = levelGameObject.GetComponent<Material>();
        SpriteRenderer spriteRenderer = levelGameObject.GetComponent<SpriteRenderer>();
        CompositeCollider2D compositeCollider = levelGameObject.GetComponent<CompositeCollider2D>();

        Dictionary<Vector2, Tile> grid = TileGrid.Grid;

        List<GameObject> gameObjectsToCombine = new List<GameObject>();
        foreach (Tile tile in grid.Values) {
            GameObject originalTileGameObject = tile.GameObject;
            GameObject duplicateTileGameObject = Object.Instantiate(originalTileGameObject);
            Object.Destroy(originalTileGameObject);
            gameObjectsToCombine.Add(duplicateTileGameObject);
        }

        List<SpriteRenderer> spriteRenderersToCombine = new List<SpriteRenderer>();
        List<BoxCollider2D> boxCollidersToCombine = new List<BoxCollider2D>();

        foreach (GameObject gameObjectToCombine in gameObjectsToCombine) {
            spriteRenderersToCombine.Add(gameObjectToCombine.GetComponent<SpriteRenderer>());
            boxCollidersToCombine.Add(gameObjectToCombine.GetComponent<BoxCollider2D>());
        }

        SpriteHelper.CombineSpritesOfGameObjects(spriteRenderersToCombine, material, spriteRenderer);
        ColliderHelper.CombineBoxCollidersInCompositeCollider(boxCollidersToCombine, compositeCollider);

        foreach (GameObject gameObjectToCombine in gameObjectsToCombine) {
            Object.Destroy(gameObjectToCombine);
        }
    }
}