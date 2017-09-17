using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CombineStandardTilesCommand : Command {

    private const string LEVEL_COLLIDER_PATH = "LevelEditor/LevelCollider";
    private const string LEVEL_VISUAL_PATH = "LevelEditor/LevelVisual";

    protected override void Execute() {
        GameObject levelColliderPrefab = Resources.Load<GameObject>(LEVEL_COLLIDER_PATH);
        GameObject levelVisualPrefab = Resources.Load<GameObject>(LEVEL_VISUAL_PATH);

        GameObject levelColliderGameObject = Object.Instantiate(levelColliderPrefab);

        Dictionary<Vector2, Tile> grid = TileGrid.Grid;

        List<GameObject> gameObjectCollidersToCombine = new List<GameObject>();
        List<GameObject> gameObjectSpriteRenderersToCombine = new List<GameObject>();
        foreach (Tile tile in grid.Values) {
            GameObject originalTileGameObject = tile.GameObject;
            GameObject duplicateTileGameObject = Object.Instantiate(originalTileGameObject);

            gameObjectCollidersToCombine.Add(duplicateTileGameObject);
            if (tile.TileType != TileType.Standard) { continue; }
            gameObjectSpriteRenderersToCombine.Add(duplicateTileGameObject);
        }

        List<SpriteRenderer> spriteRenderersToCombine = new List<SpriteRenderer>();
        List<BoxCollider2D> boxCollidersToCombine = new List<BoxCollider2D>();

        foreach (GameObject gameObjectColliderToCombine in gameObjectCollidersToCombine) {
            BoxCollider2D boxColliderToCombine = gameObjectColliderToCombine.GetComponent<BoxCollider2D>();
            boxCollidersToCombine.Add(boxColliderToCombine);
        }

        foreach (GameObject gameObjectSpriteRendererToCombine in gameObjectSpriteRenderersToCombine) {
            SpriteRenderer spriteRendererToCombine = gameObjectSpriteRendererToCombine.GetComponent<SpriteRenderer>();
            spriteRenderersToCombine.Add(spriteRendererToCombine);
        }

        GameObject levelVisualGameObject = Object.Instantiate(levelVisualPrefab, levelColliderGameObject.transform);
        Material material = levelVisualGameObject.GetComponent<Material>();
        SpriteRenderer spriteRenderer = levelVisualGameObject.GetComponent<SpriteRenderer>();

        SpriteHelper.CombineSpritesOfGameObjects(spriteRenderersToCombine, material, spriteRenderer);

        CompositeCollider2D compositeCollider = levelColliderGameObject.GetComponent<CompositeCollider2D>();
        ColliderHelper.CombineBoxCollidersInCompositeCollider(boxCollidersToCombine, compositeCollider);

        foreach (GameObject gameObjectSpriteRenderer in gameObjectSpriteRenderersToCombine) {
            Object.Destroy(gameObjectSpriteRenderer);
        }
    }
}